using Microsoft.AspNetCore.Mvc;
using Z.Ddd.Common.DomainServiceRegister;
using Z.Ddd.Common.ResultResponse;
using Z.NetWiki.Application.CommentsModule.BlogClient.Dto;
using Z.NetWiki.Domain.SharedDto;

namespace Z.NetWiki.Application.CommentsModule.BlogClient
{
    /// <summary>
    /// 文章管理
    /// </summary>
    public interface ICommentsCAppService : IApplicationService
    {
        Task<PageResult<CommentOutput>> GetList([FromBody] CommentPageQueryInput dto);


        Task<PageResult<ReplyOutput>> ReplyList([FromBody] CommentPageQueryInput dto);

        Task Add(AddCommentInput dto);
        Task Praise(KeyDto dto);


    }
}
