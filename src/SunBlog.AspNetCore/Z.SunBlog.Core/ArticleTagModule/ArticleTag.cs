using Z.Foundation.Core.Entities.Auditing;

namespace Z.SunBlog.Core.ArticleTagModule;
/// <summary>
/// 文章标签表
/// </summary>
public class ArticleTag : FullAuditedEntity<Guid>
{
    /// <summary>
    /// 文章Id
    /// </summary>
    public Guid ArticleId { get; set; }

    /// <summary>
    /// 标签Id
    /// </summary>
    public Guid TagId { get; set; }

    public ArticleTag()
    {
        
    }
    
    
    public ArticleTag(Guid articleId, Guid tagId)
    {
        ArticleId = articleId;
        TagId = tagId;
    }
}