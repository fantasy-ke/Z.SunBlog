using Z.Ddd.Common.ResultResponse;

namespace Z.SunBlog.Application.ArticleModule.BlogClient.Dto;

public class ArticleListQueryInput : Pagination
{
    /// <summary>
    /// 标签ID
    /// </summary>
    public Guid? TagId { get; set; }
    /// <summary>
    /// 栏目ID
    /// </summary>
    public Guid? CategoryId { get; set; }

    /// <summary>
    /// 关键词
    /// </summary>
    public string Keyword { get; set; }
}