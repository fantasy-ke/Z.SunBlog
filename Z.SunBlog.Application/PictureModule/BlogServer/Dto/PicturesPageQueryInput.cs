using Z.Ddd.Common.ResultResponse;

namespace Z.SunBlog.Application.PictureModule.BlogServer.Dto;

public class PicturesPageQueryInput : Pagination
{
    /// <summary>
    /// 相册ID
    /// </summary>
    public Guid Id { get; set; }
}