using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Ddd.Common.DomainServiceRegister;
using Z.Ddd.Common.Entities.Organizations;
using Z.Ddd.Common.Entities.Repositories;
using Z.Ddd.Common.Extensions;
using Z.Ddd.Common.ResultResponse;
using Z.EntityFrameworkCore.Extensions;
using Z.Module.DependencyInjection;
using Z.SunBlog.Application.SystemServiceModule.UserService.Dto;
using Z.SunBlog.Core.Const;
using Z.SunBlog.Core.CustomConfigModule;
using Z.SunBlog.Core.UserModule.DomainManager;

namespace Z.SunBlog.Application.SystemServiceModule.UserService
{
    public interface IUserSysAppService : IApplicationService, ITransientDependency
    {

    }
    /// <summary>
    /// 用户操作服务
    /// </summary>
    public class UserSysAppService : ApplicationService, IUserSysAppService
    {
        private readonly IBasicRepository<ZOrganization> _orgRepository;
        private readonly IUserDomainManager _userDomainManager;
        public UserSysAppService(IServiceProvider serviceProvider, IBasicRepository<ZOrganization> orgRepository, IUserDomainManager userDomainManager) : base(serviceProvider)
        {
            _orgRepository = orgRepository;
            _userDomainManager = userDomainManager;
        }

        /// <summary>
        /// 系统用户分页查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [DisplayName("系统用户分页查询")]
        [HttpGet]
        public async Task<PageResult<SysUserPageOutput>> Page([FromQuery] QuerySysUserInput dto)
        {
            List<string> orgIdList = new List<string>();
            if (!string.IsNullOrWhiteSpace(dto.OrgId))
            {
                orgIdList.Add(dto.OrgId);
                var list = await _orgRepository.GetQueryAll().Include(p => p.Children).Where(p=>p.ParentId == dto.OrgId).ToListAsync();
                orgIdList.AddRange(list.Select(x => x.Id));
            }
            return await _userDomainManager.QueryAsNoTracking
                .WhereIf(!string.IsNullOrWhiteSpace(dto.Name), x => x.Name.Contains(dto.Name))
                .WhereIf(!string.IsNullOrWhiteSpace(dto.UserName), x => x.UserName.Contains(dto.UserName))
                .WhereIf(!string.IsNullOrWhiteSpace(dto.Mobile), x => x.Mobile.Contains(dto.Mobile))
                .WhereIf(orgIdList.Any(), x => orgIdList.Contains(x.OrgId))
                .Select(x => new SysUserPageOutput
                {
                    Name = x.Name,
                    Status = x.Status,
                    UserName = x.UserName,
                    Birthday = x.Birthday,
                    Mobile = x.Mobile,
                    Gender = x.Gender,
                    NickName = x.Name,
                    CreatedTime = x.CreationTime,
                    Email = x.Email,
                    Id = x.Id
                }).ToPagedListAsync(dto);
        }

        /// <summary>
        /// 添加系统用户
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [UnitOfWork, HttpPost("add")]
        [DisplayName("添加系统用户")]
        public async Task AddUser(AddSysUserInput dto)
        {
            var user = dto.Adapt<SysUser>();
            user.Id = _idGenerator.NextId();
            string encode = _idGenerator.Encode(user.Id);
            var setting = await _customConfigService.Get<SysSecuritySetting>();
            user.Password = MD5Encryption.Encrypt(encode + (setting?.Password ?? "123456"));
            var roles = dto.Roles.Select(x => new SysUserRole()
            {
                RoleId = x,
                UserId = user.Id
            }).ToList();
            await _repository.InsertAsync(user);
            await _userRoleRepository.InsertRangeAsync(roles);
        }

        /// <summary>
        /// 更新系统用户信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [DisplayName("更新系统用户信息")]
        [UnitOfWork, HttpPut("edit")]
        public async Task UpdateUser(UpdateSysUserInput dto)
        {
            var user = await _repository.GetByIdAsync(dto.Id);
            if (user == null) throw Oops.Bah("无效参数");

            dto.Adapt(user);
            var roles = dto.Roles.Select(x => new SysUserRole()
            {
                RoleId = x,
                UserId = user.Id
            }).ToList();
            await _repository.UpdateAsync(user);
            await _userRoleRepository.DeleteAsync(x => x.UserId == user.Id);
            await _userRoleRepository.InsertRangeAsync(roles);
            await _easyCachingProvider.RemoveByPrefixAsync(CacheConst.PermissionKey);
        }

        /// <summary>
        /// 系统用户详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<UpdateSysUserInput> Detail([FromQuery] long id)
        {
            return await _repository.AsQueryable().Where(x => x.Id == id)
                  .Select(x => new UpdateSysUserInput()
                  {
                      Id = x.Id,
                      Name = x.Name,
                      Status = x.Status,
                      OrgId = x.OrgId,
                      Account = x.Account,
                      Mobile = x.Mobile,
                      Remark = x.Remark,
                      Birthday = x.Birthday,
                      Email = x.Email,
                      Gender = x.Gender,
                      NickName = x.NickName,
                      Roles = SqlFunc.Subqueryable<SysUserRole>().Where(s => s.UserId == x.Id).ToList(s => s.RoleId)
                  }).FirstAsync();
        }

        /// <summary>
        /// 重置系统用户密码
        /// </summary>
        /// <returns></returns>
        [DisplayName("重置系统用户密码")]
        [HttpPatch]
        public async Task Reset(ResetPasswordInput dto)
        {
            string encrypt = MD5Encryption.Encrypt(_idGenerator.Encode(dto.Id) + dto.Password);
            await _repository.UpdateAsync(x => new SysUser()
            {
                Password = encrypt
            }, x => x.Id == dto.Id);
        }

        /// <summary>
        /// 获取当前登录用户的信息
        /// </summary>
        /// <returns></returns>
        [DisplayName("获取登录用户的信息")]
        [HttpGet]
        public async Task<SysUserInfoOutput> CurrentUserInfo()
        {
            var userId = _authManager.UserId;
            return await _repository.AsQueryable().Where(x => x.Id == userId)
                  .Select(x => new SysUserInfoOutput
                  {
                      Name = x.Name,
                      Account = x.Account,
                      Avatar = x.Avatar,
                      Birthday = x.Birthday,
                      Email = x.Email,
                      Gender = x.Gender,
                      NickName = x.NickName,
                      Remark = x.Remark,
                      LastLoginIp = x.LastLoginIp,
                      LastLoginAddress = x.LastLoginAddress,
                      Mobile = x.Mobile,
                      OrgId = x.OrgId,
                      OrgName = SqlFunc.Subqueryable<SysOrganization>().Where(o => o.Id == x.OrgId).Select(o => o.Name)
                  })
                  .Mapper(dto =>
                  {
                      if (_authManager.IsSuperAdmin)
                      {
                          dto.AuthBtnList = _repository.AsSugarClient().Queryable<SysMenu>().Where(x => x.Type == MenuType.Button)
                                .Select(x => x.Code).ToList();
                      }
                      else
                      {
                          var list = _sysMenuService.GetAuthButtonCodeList(userId).GetAwaiter().GetResult();
                          dto.AuthBtnList = list.Where(x => x.Access).Select(x => x.Code).ToList();
                      }
                  })
                  .FirstAsync();
        }

        /// <summary>
        /// 用户修改账户密码
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [DisplayName("用户修改账户密码")]
        [HttpPatch]
        public async Task ChangePassword(ChangePasswordOutput dto)
        {
            var userId = _authManager.UserId;
            string encode = _idGenerator.Encode(userId);
            string pwd = MD5Encryption.Encrypt($"{encode}{dto.OriginalPwd}");
            if (!await _repository.IsAnyAsync(x => x.Id == userId && x.Password == pwd))
            {
                throw Oops.Bah("原密码错误");
            }
            pwd = MD5Encryption.Encrypt($"{encode}{dto.Password}");
            await _repository.AsSugarClient().Updateable<SysUser>()
                .SetColumns(x => new SysUser()
                {
                    Password = pwd
                })
                .Where(x => x.Id == userId)
                .ExecuteCommandHasChangeAsync();
        }

        /// <summary>
        /// 用户修改头像
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [DisplayName("用户修改头像")]
        [HttpPatch]
        public async Task UploadAvatar([FromBody] string url)
        {
            long userId = _authManager.UserId;
            await _repository.UpdateAsync(x => new SysUser()
            {
                Avatar = url
            }, x => x.Id == userId);
        }

        /// <summary>
        /// 系统用户修改自己的信息
        /// </summary>
        /// <returns></returns>
        [DisplayName("系统用户修改个人信息")]
        [HttpPatch("updateCurrentUser")]
        public async Task UpdateCurrentUser(UpdateCurrentUserInput dto)
        {
            long userId = _authManager.UserId;
            await _repository.UpdateAsync(x => new SysUser()
            {
                Name = dto.Name,
                Birthday = dto.Birthday,
                Email = dto.Email,
                Gender = dto.Gender,
                Mobile = dto.Mobile,
                NickName = dto.NickName
            }, x => x.Id == userId);
        }
    }
}
