using Microsoft.AspNetCore.Mvc;
using Z.Ddd.Common.DomainServiceRegister;
using Z.Ddd.Common.ResultResponse;
using Z.SunBlog.Application.AlbumsModule.BlogServer.Dto;

namespace Z.SunBlog.Application.AlbumsModule.BlogServer
{
    /// <summary>
    /// 相册管理
    /// </summary>
    public interface IAlbumsSAppService : IApplicationService
    {
        Task<PageResult<AlbumsPageOutput>> GetPage([FromBody] AlbumsPageQueryInput dto);

        Task CreateOrUpdate(CreateOrUpdateAlbumsInput dto);

    }
}
