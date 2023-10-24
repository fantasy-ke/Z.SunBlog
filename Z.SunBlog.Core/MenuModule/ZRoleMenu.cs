using Z.Ddd.Common.Entities.Auditing;

namespace Easy.Admin.Core.Entities;

/// <summary>
/// 角色权限表
/// </summary>
public class ZRoleMenu : FullAuditedEntity<Guid>
{
    /// <summary>
    /// 角色id
    /// </summary>
    public string RoleId { get; set; }

    /// <summary>
    /// 菜单按钮id
    /// </summary>
    public Guid MenuId { get; set; }
}