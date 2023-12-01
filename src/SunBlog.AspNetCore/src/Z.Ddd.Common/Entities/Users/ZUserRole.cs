using Z.Ddd.Common.Entities.Auditing;

namespace Z.Ddd.Common.Entities.Users;

public class ZUserRole : FullAuditedEntity<Guid>
{
    /// <summary>
    /// 用户id
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    /// 角色id
    /// </summary>
    public string RoleId { get; set; }
}