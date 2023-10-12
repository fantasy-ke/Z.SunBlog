using Z.NetWiki.Domain.Enum;

namespace Z.NetWiki.Domain.SharedDto;

public class AvailabilityDto : KeyDto
{
    /// <summary>
    /// 状态
    /// </summary>
    public AvailabilityStatus Status { get; set; }
}