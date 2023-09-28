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

            // var dfs = await _userDomainManager.QueryAsNoTracking.ToListAsync();

            await _userDomainManager.Delete("d8be51d9-a1c7-4073-9e56-08dbbfc29a33");

            await _userDomainManager.Create(new ZUserInfo
            {
                UserName = "小周2",
                Name = "科",
                PassWord = "222"
            });
        }
    }


}
