using System.ComponentModel.DataAnnotations;

namespace Z.SunBlog.Application.PictureModule.BlogServer.Dto;

public class AddPictureInput
{
    /// <summary>
    /// 相册Id
    /// </summary>
    [Required(ErrorMessage = "缺少必要参数")]
    public Guid? AlbumId { get; set; }

    /// <summary>
    /// 图片地址
    /// </summary>
    [Required(ErrorMessage = "请上传图片")]
    public string? Url { get; set; }
}