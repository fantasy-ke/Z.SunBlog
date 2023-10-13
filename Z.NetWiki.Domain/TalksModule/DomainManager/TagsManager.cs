using Z.Ddd.Common.DomainServiceRegister;

namespace Z.NetWiki.Domain.TalksModule.DomainManager
{
    public class TalksManager : BusinessDomainService<Talks>, ITalksManager
    {
        public TalksManager(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override async Task ValidateOnCreateOrUpdate(Talks entity)
        {
            await Task.CompletedTask;
        }

    }
}
