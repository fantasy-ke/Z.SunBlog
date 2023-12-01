using System.ComponentModel.DataAnnotations;

namespace Z.SunBlog.Application.ConfigModule.Dto;

public class AddCustomConfigItemInput
{
    /// <summary>
    /// 自定义配置Id
    /// </summary>
    [Required(ErrorMessage = "缺少配置参数")]
    public Guid ConfigId { get; set; }

    /// <summary>
    /// 配置
    /// </summary>
    [Required(ErrorMessage = "配置内容不可为空")]
    public string Json { get; set; }
}