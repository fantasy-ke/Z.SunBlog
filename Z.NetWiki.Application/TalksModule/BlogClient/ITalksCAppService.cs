using Microsoft.AspNetCore.Mvc;
using Z.Ddd.Common.DomainServiceRegister;
using Z.Ddd.Common.ResultResponse;
using Z.NetWiki.Application.AlbumsModule.BlogClient.Dto;
using Z.NetWiki.Application.ArticleModule.BlogClient.Dto;
using Z.NetWiki.Application.TalksModule.BlogClient.Dto;

namespace Z.NetWiki.Application.TalksModule.BlogClient
{
    /// <summary>
    /// 博客说说
    /// </summary>
    public interface ITalksCAppService : IApplicationService
    {
        Task<PageResult<TalksOutput>> GetList([FromBody] Pagination dto);

        Task<TalkDetailOutput> TalkDetail([FromQuery] Guid id);
    }
}
