using Z.Ddd.Common.ResultResponse;

namespace Z.NetWiki.Application.TalksModule.BlogServer.Dto;

public class TalksPageQueryInput : Pagination
{
    /// <summary>
    /// 关键词
    /// </summary>
    public string Keyword { get; set; }
}