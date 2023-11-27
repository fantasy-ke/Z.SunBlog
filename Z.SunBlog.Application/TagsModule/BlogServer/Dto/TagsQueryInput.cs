using Z.Ddd.Common.ResultResponse.Pager;

namespace Z.SunBlog.Application.TagsModule.BlogServer.Dto;

public class TagsPageQueryInput : Pagination
{
    /// <summary>
    /// 标签名称 
    /// </summary>
    public string? Name { get; set; }
}