using Microsoft.AspNetCore.Mvc;
using Z.Ddd.Common.DomainServiceRegister;
using Z.Ddd.Common.ResultResponse;
using Z.NetWiki.Application.ArticleModule.BlogServer.Dto;
using Z.NetWiki.Domain.SharedDto;

namespace Z.NetWiki.Application.ArticleModule.BlogServer
{
    /// <summary>
    /// 文章管理
    /// </summary>
    public interface IArticleSAppService : IApplicationService
    {
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task DeleteAsync(KeyDto dto);

        /// <summary>
        /// 文章列表分页查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PageResult<ArticlePageOutput>> GetPage([FromQuery] ArticlePageQueryInput dto);

        /// <summary>
        /// 创建修改
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task CreateOrUpdate(CreateOrUpdateArticleInput dto);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        Task<ArticleDetailOutput> GetDetail([FromQuery] Guid id);

    }
}
