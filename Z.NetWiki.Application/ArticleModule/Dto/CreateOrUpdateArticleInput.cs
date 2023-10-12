using System.ComponentModel.DataAnnotations;

namespace Z.NetWiki.Application.ArticleModule.Dto;

public class CreateOrUpdateArticleInput : AddArticleInput
{
    /// <summary>
    /// 文章ID
    /// </summary>
    public Guid? Id { get; set; }
}