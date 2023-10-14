using Z.Ddd.Common.DomainServiceRegister;
using Z.NetWiki.Core.TagModule;
using Z.NetWiki.Core.TagsModule.DomainManager;

namespace Z.NetWiki.Core.TagsModule.DomainManager
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
