using Z.Ddd.Common.ResultResponse;

namespace Z.SunBlog.Application.FriendLinkModule.BlogServer.Dto;

public class FriendLinkPageQueryInput : Pagination
{
    /// <summary>
    /// 站点名称
    /// </summary>
    public string? SiteName { get; set; }
}