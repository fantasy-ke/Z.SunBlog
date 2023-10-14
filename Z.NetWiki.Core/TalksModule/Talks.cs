
using System.ComponentModel.DataAnnotations;
using Z.Ddd.Common.Entities.Auditing;
using Z.NetWiki.Core.Enum;

namespace Z.NetWiki.Core.TalksModule;

public class Talks : FullAuditedEntity<Guid>
{
    /// <summary>
    /// 是否置顶
    /// </summary>
    public bool IsTop { get; set; }

    /// <summary>
    /// 说说正文
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// 图片
    /// </summary>
    [MaxLength(2048)]
    public string? Images { get; set; }

    /// <summary>
    /// 是否允许评论
    /// </summary>
    public bool IsAllowComments { get; set; }

    /// <summary>
    /// 可用状态
    /// </summary>
    public AvailabilityStatus Status { get; set; }
}