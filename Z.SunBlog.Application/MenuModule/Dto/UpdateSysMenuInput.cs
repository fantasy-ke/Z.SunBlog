namespace Z.SunBlog.Application.MenuModule.Dto;

public class UpdateSysMenuInput : AddSysMenuInput
{
    /// <summary>
    /// 菜单/按钮Id
    /// </summary>
    public Guid Id { get; set; }
}