using Z.Fantasy.Core.DomainServiceRegister;
using Z.Foundation.Core.Entities.Users;
using Z.SunBlog.Application.UserModule.Dto;

namespace Z.SunBlog.Application.UserModule
{
    public interface IUserAppService : IApplicationService
    {
        Task<ZUserInfo> Create();

        Task<List<ZUserInfoDto>> GetFrist();

		Task<ZUserInfoDto?> Login(ZUserInfoDto user);

        Task<ZUserInfoOutput> CurrentUserInfo();

    }
}
