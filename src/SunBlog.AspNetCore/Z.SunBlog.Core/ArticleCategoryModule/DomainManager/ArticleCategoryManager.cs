using Z.Fantasy.Core.DomainServiceRegister.Domain;

namespace Z.SunBlog.Core.ArticleCategoryModule.DomainManager
{
    public class ArticleCategoryManager : BusinessDomainService<ArticleCategory>, IArticleCategoryManager
    {
        public ArticleCategoryManager(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override async Task ValidateOnCreateOrUpdate(ArticleCategory entity)
        {
            await Task.CompletedTask;
        }

    }
}
