using Microsoft.AspNetCore.Mvc;
using Z.Ddd.Common.DomainServiceRegister;
using Z.Ddd.Common.ResultResponse;
using Z.NetWiki.Application.ArticleModule.BlogClient.Dto;

namespace Z.NetWiki.Application.ArticleModule.BlogClient
{
    /// <summary>
    /// 文章管理
    /// </summary>
    public interface IArticleCAppService : IApplicationService
    {
        Task<PageResult<ArticleOutput>> GetList([FromBody] ArticleListQueryInput dto);

        Task<List<TagsOutput>> Tags();

        Task<List<CategoryOutput>> Categories();

        Task<ArticleReportOutput> ReportStatistics();

        Task<ArticleInfoOutput> Info([FromQuery] Guid id);

        Task<List<ArticleBasicsOutput>> Latest();

    }
}
