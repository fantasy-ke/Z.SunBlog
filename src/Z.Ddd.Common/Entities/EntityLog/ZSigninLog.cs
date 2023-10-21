using System.ComponentModel.DataAnnotations;
using Z.Ddd.Common.Entities.Auditing;

namespace Z.Ddd.Common.Entities.EntityLog;
/// <summary>
/// 用户登录日志
/// </summary>
public class ZSigninLog : CreationAuditedEntity<Guid>
{
    /// <summary>
    /// 用户ID
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    /// ip地址
    /// </summary>

    [MaxLength(64)]
    public string? RemoteIp { get; set; }

    /// <summary>
    /// 浏览器
    /// </summary>
    [MaxLength(256)]
    public string? UserAgent { get; set; }

    /// <summary>
    /// Ip归属地
    /// </summary>
    [MaxLength(128)]
    public string? Location { get; set; }

    /// <summary>
    /// 用户系统信息
    /// </summary>
    [MaxLength(256)]
    public string? OsDescription { get; set; }

    /// <summary>
    /// 消息
    /// </summary>
    [MaxLength(256)]
    public string? Message { get; set; }
}