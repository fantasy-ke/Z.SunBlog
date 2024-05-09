using System.ComponentModel.DataAnnotations;
using Z.Foundation.Core.Entities.Auditing;
using Z.Foundation.Core.Entities.Enum;

namespace Z.Foundation.Core.Entities.Roles;

/// <summary>
/// 角色表
/// </summary>
public class ZRoleInfo : FullAuditedEntity<string>
{
    /// <summary>
    /// 角色名
    /// </summary>
    [MaxLength(32)]
    public string Name { get; set; }

    /// <summary>
    /// 角色编码
    /// </summary>
    [MaxLength(32)]
    public string Code { get; set; }

    /// <summary>
    /// 可用状态
    /// </summary>
    public AvailabilityStatus Status { get; set; }

    /// <summary>
    /// 排序值
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [MaxLength(256)]
    public string Remark { get; set; }
    
    /// <summary>
    /// 设置角色id
    /// 
    /// </summary>
    /// <param name="roleId"></param>
    public void SetRoleId(string roleId)
    {
        Id = roleId;
    }
    
    public void SetStatus(AvailabilityStatus status)
    {
        Status = status;
    }
}
