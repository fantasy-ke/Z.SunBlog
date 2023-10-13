using Z.Ddd.Common.DomainServiceRegister;

namespace Z.NetWiki.Domain.CommentsModule.DomainManager
{
    public class CommentsManager : BusinessDomainService<Comments>, ICommentsManager
    {
        public CommentsManager(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override async Task ValidateOnCreateOrUpdate(Comments entity)
        {
            await Task.CompletedTask;
        }

    }
}
