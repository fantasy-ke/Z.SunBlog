using System.ComponentModel.DataAnnotations;

namespace Z.SunBlog.Application.ArticleModule.BlogServer.Dto;

public class CreateOrUpdateArticleInput : AddArticleInput
{
    /// <summary>
    /// 文章ID
    /// </summary>
    public Guid? Id { get; set; }
}