using Z.Ddd.Common.DomainServiceRegister;
using Z.SunBlog.Core.TagModule;
using Z.SunBlog.Core.TagsModule.DomainManager;

namespace Z.SunBlog.Core.TagsModule.DomainManager
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
