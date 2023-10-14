using Z.Ddd.Common.DomainServiceRegister;

namespace Z.NetWiki.Core.AlbumsModule.DomainManager
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
