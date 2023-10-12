using Z.Ddd.Common.Entities.Auditing;

namespace Z.NetWiki.Domain.ArticleCategoryModule;
/// <summary>
/// 文章所属栏目表
/// </summary>
public class ArticleCategory : FullAuditedEntity<Guid>
{
    /// <summary>
    /// 文章Id
    /// </summary>
    public Guid ArticleId { get; set; }

    /// <summary>
    /// 栏目Id
    /// </summary>
    public Guid CategoryId { get; set; }
}