using Z.Ddd.Common.DomainServiceRegister;

namespace Z.NetWiki.Domain.AlbumsModule.DomainManager
{
    public class AlbumsManager : BusinessDomainService<Albums>, IAlbumsManager
    {
        public AlbumsManager(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override async Task ValidateOnCreateOrUpdate(Albums entity)
        {
            await Task.CompletedTask;
        }

    }
}
