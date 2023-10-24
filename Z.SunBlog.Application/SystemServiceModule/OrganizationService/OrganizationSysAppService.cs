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
using Z.Ddd.Common.Entities.Roles;
using Z.Ddd.Common.Exceptions;
using Z.Ddd.Common.ResultResponse;
using Z.Module.DependencyInjection;
using Z.SunBlog.Application.SystemServiceModule.OrganizationService.Dto;
using Z.SunBlog.Application.SystemServiceModule.RoleService.Dto;
using Z.SunBlog.Application.SystemServiceModule.UserService;
using Z.SunBlog.Core.SharedDto;

namespace Z.SunBlog.Application.SystemServiceModule.OrganizationService
{
    /// <summary>
    /// 用户后台操作接口
    /// </summary>
    public interface IOrganizationSysAppService : IApplicationService, ITransientDependency
    {
        Task<List<SysOrgPageOutput>> GetPage([FromQuery] string name);
        Task AddOrg(AddOrgInput dto);

        Task UpdateOrg(UpdateOrgInput dto);

        Task<List<TreeSelectOutput>> TreeSelect();
    }
    internal class OrganizationSysAppService : ApplicationService, IOrganizationSysAppService
    {
        private readonly IBasicDomainService<ZOrganization, string> _orgDomainService;
        public OrganizationSysAppService(IServiceProvider serviceProvider, IBasicDomainService<ZOrganization, string> orgDomainService) : base(serviceProvider)
        {
            _orgDomainService = orgDomainService;
        }

        private async Task<List<ZOrganization>> GetChildOrg(List<ZOrganization> orgPanentLists, List<ZOrganization> orgtLists)
        {
            foreach (var orgPan in orgPanentLists)
            {
                var orgList = await _orgDomainService.QueryAsNoTracking
                    .Where(p => p.ParentId == orgPan.Id).ToListAsync();
                orgtLists.AddRange(orgList);
                if (orgList.Any())
                {
                    orgPan.Children = orgList;
                    orgtLists.Add(orgPan);
                    return await GetChildOrg(orgList, orgtLists);
                }
            }

            return orgtLists;
        }

        /// <summary>
        /// 组织机构列表查询
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [Description("组织机构列表查询")]
        [HttpGet]
        public async Task<List<SysOrgPageOutput>> GetPage([FromQuery] string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                var list = await _orgDomainService.QueryAsNoTracking.Where(x => x.Name.Contains(name)).ToListAsync();
                return ObjectMapper.Map<List<SysOrgPageOutput>>(list);
            }

            var treePanentList = await _orgDomainService.QueryAsNoTracking.OrderBy(x => x.Sort)
                .Where(p => p.ParentId == null)
                .ToListAsync();

            var listChild = await GetChildOrg(treePanentList, new List<ZOrganization>());

            return ObjectMapper.Map<List<SysOrgPageOutput>>(listChild);
        }

        /// <summary>
        /// 添加组织机构
        /// </summary>
        /// <returns></returns>
        [Description("添加组织机构")]
        [HttpPost]
        public async Task AddOrg(AddOrgInput dto)
        {
            var organization = ObjectMapper.Map<ZOrganization>(dto);
            await _orgDomainService.Create(organization);
        }

        /// <summary>
        /// 更新组织机构
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [Description("更新组织机构")]
        [HttpPut("edit")]
        public async Task UpdateOrg(UpdateOrgInput dto)
        {
            var organization = await _orgDomainService.FindByIdAsync(dto.Id);
            if (organization == null)
            {
                throw new UserFriendlyException("无效参数");
            }

            ObjectMapper.Map(dto, organization);
            await _orgDomainService.Update(organization);
        }

        /// <summary>
        /// 获取机构下拉选项
        /// </summary>
        /// <returns></returns>
        [Description("获取机构下拉选项")]
        [HttpGet]
        public async Task<List<TreeSelectOutput>> TreeSelect()
        {
            var treePanentList = await _orgDomainService.QueryAsNoTracking.OrderBy(x => x.Sort)
                .Where(p => p.ParentId == null)
                .ToListAsync();

            var listChild = await GetChildOrg(treePanentList, new List<ZOrganization>());

            return ObjectMapper.Map<List<TreeSelectOutput>>(listChild);
        }
    }
}
