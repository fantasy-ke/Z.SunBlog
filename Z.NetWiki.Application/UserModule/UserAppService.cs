using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Ddd.Common.DomainServiceRegister;
using Z.Ddd.Common.Entities.Users;
using Z.Ddd.Common.Extensions;
using Z.NetWiki.Application.UserModule.Dto;
using Z.NetWiki.Core.UserModule.DomainManager;

namespace Z.NetWiki.Application.UserModule
{
    /// <summary>
    /// 用户服务
    /// </summary>
	public class UserAppService : ApplicationService, IUserAppService
    {
        public readonly IUserDomainManager _userDomainManager;
        public UserAppService(IUserDomainManager userDomainManager,
            IServiceProvider serviceProvider):base(serviceProvider)
        {
            _userDomainManager = userDomainManager;
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <returns></returns>
        public async Task<ZUserInfo> Create()
        {

            
            var dfs = await _userDomainManager.QueryAsNoTracking.FirstOrDefaultAsync();

            //await _userDomainManager.Delete("6e37cc6e9b1948dba987d07b25ffc138");
            //await _userDomainManager.Delete("acab70064e8a45a0bef2074b42d9165e");
            var df1 = ObjectMapper.Map<ZUserInfoDto>(dfs);
            HttpExtension.Fill(new { df1.UserName, df1.PassWord });
            return dfs;
        }

        /// <summary>
        /// 查询一个用户
        /// </summary>
        /// <returns></returns>
        public async Task<List<ZUserInfoDto>> GetFrist()
        {
            var dfs = await _userDomainManager.QueryAsNoTracking.ToListAsync();

            return  ObjectMapper.Map<List<ZUserInfoDto>>(dfs);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
		public async Task<ZUserInfoDto?> Login(ZUserInfoDto user)
		{
			var dfs = await _userDomainManager.QueryAsNoTracking.FirstOrDefaultAsync(P=>P.UserName == user.UserName && P.PassWord == user.PassWord);
			
            if (dfs == null)
                return default;

            return ObjectMapper.Map<ZUserInfoDto>(dfs);
		}
	}


}
