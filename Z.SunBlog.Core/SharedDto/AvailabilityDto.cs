using Z.SunBlog.Core.Enum;

namespace Z.SunBlog.Core.SharedDto;

public class AvailabilityDto : KeyDto
{
    /// <summary>
    /// 状态
    /// </summary>
    public AvailabilityStatus Status { get; set; }
}