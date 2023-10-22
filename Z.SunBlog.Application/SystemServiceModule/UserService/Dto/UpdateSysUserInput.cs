namespace Z.SunBlog.Application.SystemServiceModule.UserService.Dto;

public class UpdateSysUserInput : AddSysUserInput
{
    /// <summary>
    /// 用户Id
    /// </summary>
    public string? Id { get; set; }
}