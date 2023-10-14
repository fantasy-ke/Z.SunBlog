using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Ddd.Common.DomainServiceRegister;
using Z.Ddd.Common.Entities.Users;
using Z.Ddd.Common.Exceptions;

namespace Z.NetWiki.Core.AuthAccountModule.DomainManager
{
    public class AuthAccountDomainManager : BasicDomainService<AuthAccount, string>, IAuthAccountDomainManager
    {
        public AuthAccountDomainManager(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override async Task ValidateOnCreateOrUpdate(AuthAccount entity)
        {
           await Task.CompletedTask;
        }
    }
}
