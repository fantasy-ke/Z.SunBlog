using Z.Ddd.Common.ResultResponse;

namespace Z.NetWiki.Application.ArticleModule.BlogClient.Dto;

public class ArticleListQueryInput : Pagination
{
    /// <summary>
    /// 标签ID
    /// </summary>
    public long? TagId { get; set; }
    /// <summary>
    /// 栏目ID
    /// </summary>
    public long? CategoryId { get; set; }

    /// <summary>
    /// 关键词
    /// </summary>
    public string Keyword { get; set; }
}