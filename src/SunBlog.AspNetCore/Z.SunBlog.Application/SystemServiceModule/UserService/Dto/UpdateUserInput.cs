namespace Z.SunBlog.Application.SystemServiceModule.UserService.Dto;

public class UpdateUserInput : AddUserInput
{
    /// <summary>
    /// 用户Id
    /// </summary>
    public string? Id { get; set; }
}