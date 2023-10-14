using Z.NetWiki.Core.Enum;

namespace Z.NetWiki.Core.SharedDto;

public class AvailabilityDto : KeyDto
{
    /// <summary>
    /// 状态
    /// </summary>
    public AvailabilityStatus Status { get; set; }
}