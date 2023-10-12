using Z.Ddd.Common.DomainServiceRegister;

namespace Z.NetWiki.Domain.ArticleTagModule.DomainManager
{
    public class ArticleTagManager : BusinessDomainService<ArticleTag>, IArticleTagManager
    {
        public ArticleTagManager(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override async Task ValidateOnCreateOrUpdate(ArticleTag entity)
        {
            await Task.CompletedTask;
        }

    }
}
