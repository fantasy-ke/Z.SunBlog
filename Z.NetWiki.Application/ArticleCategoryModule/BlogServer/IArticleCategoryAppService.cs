using Z.Ddd.Common.DomainServiceRegister;
using Z.NetWiki.Application.ArticleCategoryModule.BlogServer.Dto;

namespace Z.NetWiki.Application.ArticleCategoryModule.BlogServer
{
    /// <summary>
    /// 文章所属栏目管理
    /// </summary>
    public interface IArticleCategoryAppService : IApplicationService
    {
        Task Create(CreateOrUpdateArticleCategoryDto dto);


        Task Update(CreateOrUpdateArticleCategoryDto dto);
    }
}
