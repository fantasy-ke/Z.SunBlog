using System.ComponentModel.DataAnnotations;

namespace Z.SunBlog.Application.CategoryModule.BlogServer.Dto;

public class UpdateCategoryInput : AddCategoryInput
{
    /// <summary>
    /// 栏目ID
    /// </summary>
    [Required(ErrorMessage = "缺少必要参数")]
    public Guid Id { get; set; }
}