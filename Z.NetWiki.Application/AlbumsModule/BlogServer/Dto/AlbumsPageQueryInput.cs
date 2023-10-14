using Z.Ddd.Common.ResultResponse;
using Z.NetWiki.Core.AlbumsModule;

namespace Z.NetWiki.Application.AlbumsModule.BlogServer.Dto;

public class AlbumsPageQueryInput : Pagination
{
    /// <summary>
    /// 相册名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 相册类型
    /// </summary>
    public CoverType? Type { get; set; }
}