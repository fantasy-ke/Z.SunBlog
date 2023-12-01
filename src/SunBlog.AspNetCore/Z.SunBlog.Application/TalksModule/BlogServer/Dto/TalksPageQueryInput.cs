using Z.Ddd.Common.ResultResponse.Pager;

namespace Z.SunBlog.Application.TalksModule.BlogServer.Dto;

public class TalksPageQueryInput : Pagination
{
    /// <summary>
    /// 关键词
    /// </summary>
    public string? Keyword { get; set; }
}