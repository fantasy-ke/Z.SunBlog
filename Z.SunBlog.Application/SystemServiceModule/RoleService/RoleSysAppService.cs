using Easy.Admin.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yitter.IdGenerator;
using Z.Ddd.Common;
using Z.Ddd.Common.DomainServiceRegister;
using Z.Ddd.Common.Entities.Enum;
using Z.Ddd.Common.Entities.Repositories;
using Z.Ddd.Common.Entities.Roles;
using Z.Ddd.Common.Exceptions;
using Z.Ddd.Common.RedisModule;
using Z.Ddd.Common.ResultResponse;
using Z.EntityFrameworkCore.Core;
using Z.EntityFrameworkCore.Extensions;
using Z.Module.DependencyInjection;
using Z.SunBlog.Application.SystemServiceModule.RoleService.Dto;
using Z.SunBlog.Application.SystemServiceModule.UserService;
using Z.SunBlog.Application.SystemServiceModule.UserService.Dto;
using Z.SunBlog.Core.Const;
using Z.SunBlog.Core.MenuModule;
using Z.SunBlog.Core.MenuModule.DomainManager;
using Z.SunBlog.Core.SharedDto;

namespace Z.SunBlog.Application.SystemServiceModule.RoleService
{
    /// <summary>
    /// 用户后台操作接口
    /// </summary>
    public interface IRoleSysAppService : IApplicationService, ITransientDependency
    {
        Task<PageResult<SysRolePageOutput>> GetPage([FromQuery] SysRoleQueryInput dto);

        Task AddRole(AddSysRoleInput dto);

        Task UpdateRole(UpdateSysRoleInput dto);

        Task<List<Guid>> GetRuleMenu([FromQuery] string id);

        Task SetStatus(AvailabilityDto dto);

        Task Delete(string id);

        Task<List<SelectOutput>> RoleSelect();
    }
    /// <summary>
    /// 用户后台系统操作
    /// </summary>
    public class RoleSysAppService : ApplicationService, IRoleSysAppService
    {
        private readonly IBasicRepository<ZRoleInfo, string> _roleRepository;
        private readonly IIdGenerator _idGenerator;
        private readonly ICacheManager _cacheManager;
        private readonly IZRoleMenuManager _roleMenuManager;
        private readonly IMenuManager _menuManager;

        public RoleSysAppService(IServiceProvider serviceProvider,
            IBasicRepository<ZRoleInfo, string> roleRepository,
            IIdGenerator idGenerator,
            IZRoleMenuManager roleMenuManager,
            ICacheManager cacheManager,
            IMenuManager menuManager) : base(serviceProvider)
        {
            _roleRepository = roleRepository;
            _idGenerator = idGenerator;
            _roleMenuManager = roleMenuManager;
            _cacheManager = cacheManager;
            _menuManager = menuManager;
        }

        /// <summary>
        /// 角色分页查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PageResult<SysRolePageOutput>> GetPage([FromQuery] SysRoleQueryInput dto)
        {
            return await _roleRepository.GetQueryAll()
                .WhereIf(!string.IsNullOrWhiteSpace(dto.Name), x => x.Name.Contains(dto.Name))
                .OrderBy(x => x.Sort)
                .OrderByDescending(x => x.Id)
                .Select(x => new SysRolePageOutput
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreatedTime = x.CreationTime,
                    Status = x.Status,
                    Code = x.Code,
                    Sort = x.Sort,
                    Remark = x.Remark
                }).ToPagedListAsync(dto);
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Description("添加角色")]
        [HttpPost("add")]
        public async Task AddRole(AddSysRoleInput dto)
        {
            if (await _roleRepository.GetQueryAll().AnyAsync(x => x.Code == dto.Code))
            {
                throw new UserFriendlyException("角色编码已存在");
            }

            var role = ObjectMapper.Map<ZRoleInfo>(dto);
            role.Id = _idGenerator.NextId();
            var roleMenus = dto.Menus.Select(x => new ZRoleMenu()
            {
                MenuId = x,
                RoleId = role.Id
            }).ToList();
            await _roleRepository.InsertAsync(role);
            await _roleMenuManager.Create(roleMenus);
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Description("更新角色")]
        [HttpPut]
        public async Task UpdateRole(UpdateSysRoleInput dto)
        {
            var sysRole = await _roleRepository.FindAsync(dto.Id);
            if (sysRole == null)
            {
                throw new UserFriendlyException("无效参数");
            }
            if (await _roleRepository.GetQueryAll().AnyAsync(x => x.Id != dto.Id && x.Code == dto.Code))
            {
                throw new UserFriendlyException("角色编码已存在");
            }

            ObjectMapper.Map(dto, sysRole);
            var roleMenus = dto.Menus.Select(x => new ZRoleMenu()
            {
                MenuId = x,
                RoleId = sysRole.Id
            }).ToList();
            await _roleRepository.UpdateAsync(sysRole);
            await _roleMenuManager.Delete(x => x.RoleId == sysRole.Id);
            await _roleMenuManager.Create(roleMenus);
            await _cacheManager.RefreshCacheAsync(CacheConst.PermissionKey);
        }

        /// <summary>
        /// 获取角色可访问的菜单和按钮id
        /// </summary>
        /// <param name="id">角色id</param>
        /// <returns></returns>
        [Description("获取角色可访问的菜单和按钮id")]
        [HttpGet("getRuleMenu")]
        public async Task<List<Guid>> GetRuleMenu([FromQuery] string id)
        {
            return await _roleRepository.GetQueryAll().Join(_roleMenuManager.QueryAsNoTracking,
                role => role.Id, roleMenu => roleMenu.RoleId,
                (role, roleMenu) => new { role = role, roleMenu = roleMenu })
                 .Join(_menuManager.QueryAsNoTracking, rm => rm.roleMenu.MenuId, menu => menu.Id, (rm, menu) => new { role = rm.role, roleMenu = rm.roleMenu, menu = menu })
                 .Where(rm => rm.role.Id == id && rm.menu.Status == AvailabilityStatus.Enable)
                 .Select(rm => rm.roleMenu.MenuId).ToListAsync();
        }

        /// <summary>
        /// 修改角色状态
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Description("修改角色状态"), HttpPut("setStatus")]
        public async Task SetStatus(AvailabilityDto dto)
        {
            var entity = await _roleRepository.GetQueryAll().FirstOrDefaultAsync(x => x.Id == dto.Id);
            entity.Status = dto.Status;
            var entityAfter = await _roleRepository.UpdateAsync(entity);
            await _cacheManager.RefreshCacheAsync(CacheConst.PermissionKey);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Description("删除角色"), HttpDelete("delete")]
        public async Task Delete(string id)
        {
            await _roleRepository.DeleteIDAsync(id);
            await _cacheManager.RefreshCacheAsync(CacheConst.PermissionKey);
        }

        /// <summary>
        /// 角色下拉选项
        /// </summary>
        /// <returns></returns>
        [Description("角色下拉选项")]
        [HttpGet]
        public async Task<List<SelectOutput>> RoleSelect()
        {
            return await _roleRepository.GetQueryAll().Where(x => x.Status == AvailabilityStatus.Enable)
                .OrderBy(x => x.Sort)
                .OrderBy(x => x.Id)
                .Select(x => new SelectOutput()
                {
                    Value2 = x.Id,
                    Label = x.Name
                }).ToListAsync();
        }
    }
}
