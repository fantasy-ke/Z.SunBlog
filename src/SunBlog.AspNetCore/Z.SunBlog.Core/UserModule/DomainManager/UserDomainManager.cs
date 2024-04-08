using Microsoft.EntityFrameworkCore;
using Z.Fantasy.Core.DomainServiceRegister;
using Z.Fantasy.Core.Entities.Users;

namespace Z.SunBlog.Core.UserModule.DomainManager
{
    public class UserDomainManager : BasicDomainService<ZUserInfo, string>, IUserDomainManager
    {
        public UserDomainManager(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override async Task ValidateOnCreateOrUpdate(ZUserInfo entity)
        {
            var count = await Query
                .Where(a => a.UserName == entity.UserName && a.Id != entity.Id).CountAsync();

            if (count > 0)
            {
                this.ThrowRepetError(entity.UserName);
            }
        }
    }
}
