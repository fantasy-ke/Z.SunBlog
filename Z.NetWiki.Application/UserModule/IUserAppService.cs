using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Ddd.Common.DomainServiceRegister;
using Z.Ddd.Common.Entities.Users;
using Z.NetWiki.Application.UserModule.Dto;

namespace Z.NetWiki.Application.UserModule
{
    public interface IUserAppService : IApplicationService
    {
        Task<ZUserInfo> Create();

        Task<List<ZUserInfoDto>> GetFrist();
    }
}
