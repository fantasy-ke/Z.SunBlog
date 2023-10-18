namespace Z.SunBlog.Application.Dto;

public class UpdateCustomConfigInput : AddCustomConfigInput
{
    /// <summary>
    /// 配置id
    /// </summary>
    public Guid Id { get; set; }
}