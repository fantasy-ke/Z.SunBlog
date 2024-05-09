using Z.Foundation.Core.Entities.Auditing;

namespace Z.SunBlog.Core.ArticleCategoryModule;
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
    
    public ArticleCategory(){}
    
    /// <summary>
    /// 新增文章栏目
    /// </summary>
    /// <param name="articleId"></param>
    /// <param name="categoryId"></param>
    public ArticleCategory(Guid articleId, Guid categoryId)
    {
        ArticleId = articleId;
        CategoryId = categoryId;
    }

    /// <summary>
    /// 修改栏目id
    /// </summary>
    /// <param name="categoryId"></param>
    public void SetCategoryId(Guid categoryId)
    {
        CategoryId = categoryId;
    }
}