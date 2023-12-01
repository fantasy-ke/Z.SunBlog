namespace Z.SunBlog.Application.SystemServiceModule.RoleService.Dto;

public class UpdateSysRoleInput : AddRoleInput
{
    /// <summary>
    /// 角色Id
    /// </summary>
    public string? Id { get; set; }
}