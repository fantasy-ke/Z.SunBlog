using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Ddd.Common.DomainServiceRegister;
using Z.Ddd.Common.Entities.Users;

namespace Z.NetWiki.Domain.UserModule.DomainManager
{
    public class UserDomainManager : BasicDomainService<ZUserInfo, Guid>, IUserDomainManager
    {
        public UserDomainManager(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
