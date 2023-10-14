using Z.Ddd.Common.ResultResponse;

namespace Z.SunBlog.Application.AlbumsModule.BlogClient.Dto;

public class PicturesQueryInput : Pagination
{
    /// <summary>
    /// 相册ID
    /// </summary>
    public Guid AlbumId { get; set; }
}