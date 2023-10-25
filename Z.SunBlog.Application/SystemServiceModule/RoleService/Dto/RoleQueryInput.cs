using Z.Ddd.Common.ResultResponse;

namespace Z.SunBlog.Application.SystemServiceModule.RoleService.Dto;

public class RoleQueryInput : Pagination
{
    /// <summary>
    /// 角色名称
    /// </summary>
    public string? Name { get; set; }
}