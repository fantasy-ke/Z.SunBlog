using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Ddd.Common.DomainServiceRegister;
using Z.Ddd.Common.Entities.Users;
using Z.NetWiki.Domain.UserModule.DomainManager;

namespace Z.NetWiki.Application.UserModule
{
    public class UserAppService : ApplicationService, IUserAppService
    {
        public readonly IUserDomainManager _userDomainManager;
        public UserAppService(IUserDomainManager userDomainManager)
        {
            _userDomainManager = userDomainManager;
        }

        public async Task Create()
        {

             var dfs = await _userDomainManager.QueryAsNoTracking.ToListAsync();

            //await _userDomainManager.Delete("6e37cc6e9b1948dba987d07b25ffc138");
            //await _userDomainManager.Delete("acab70064e8a45a0bef2074b42d9165e");

            await _userDomainManager.Create(new ZUserInfo
            {
                UserName = "小周2",
                Name = "科",
                PassWord = "222"
            });
        }
    }


}
