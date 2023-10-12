namespace Z.NetWiki.Domain.SharedDto;
/// <summary>
/// 下拉框
/// </summary>
public class SelectOutput
{
    /// <summary>
    /// 文本
    /// </summary>
    public string Label { get; set; }

    /// <summary>
    /// 值
    /// </summary>
    public Guid Value { get; set; }
}