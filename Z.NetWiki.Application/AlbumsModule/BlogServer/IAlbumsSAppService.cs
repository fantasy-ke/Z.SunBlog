using Microsoft.AspNetCore.Mvc;
using Z.Ddd.Common.DomainServiceRegister;
using Z.Ddd.Common.ResultResponse;
using Z.NetWiki.Application.AlbumsModule.BlogServer.Dto;

namespace Z.NetWiki.Application.AlbumsModule.BlogServer
{
    /// <summary>
    /// 相册管理
    /// </summary>
    public interface IAlbumsSAppService : IApplicationService
    {
        Task<PageResult<AlbumsPageOutput>> GetPage([FromQuery] AlbumsPageQueryInput dto);

        Task CreateOrUpdate(CreateOrUpdateAlbumsInput dto);

    }
}
