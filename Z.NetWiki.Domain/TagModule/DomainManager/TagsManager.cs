using Z.Ddd.Common.DomainServiceRegister;
using Z.NetWiki.Domain.TagModule;
using Z.NetWiki.Domain.TagsModule.DomainManager;

namespace Z.NetWiki.Domain.TagsModule.DomainManager
{
    public class TagsManager : BusinessDomainService<Tags>, ITagsManager
    {
        public TagsManager(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override async Task ValidateOnCreateOrUpdate(Tags entity)
        {
            await Task.CompletedTask;
        }

    }
}
