using Z.Ddd.Common.ResultResponse;

namespace Z.NetWiki.Application.TagsModule.BlogServer.Dto;

public class TagsPageQueryInput : Pagination
{
    /// <summary>
    /// 标签名称 
    /// </summary>
    public string Name { get; set; }
}