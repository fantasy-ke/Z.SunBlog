using System.ComponentModel.DataAnnotations;

namespace Z.SunBlog.Application.ConfigModule.Dto;

public class UpdateCustomConfigItemInput : AddCustomConfigItemInput
{
    [Required(ErrorMessage = "缺少必要参数")]
    public Guid Id { get; set; }
}