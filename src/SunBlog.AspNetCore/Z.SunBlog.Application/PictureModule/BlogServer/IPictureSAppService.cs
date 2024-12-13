using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Z.Fantasy.Core.DomainServiceRegister;
using Z.Fantasy.Core.ResultResponse.Pager;
using Z.SunBlog.Application.PictureModule.BlogServer.Dto;
using Z.SunBlog.Core.SharedDto;

namespace Z.SunBlog.Application.PictureModule.BlogServer
{
    /// <summary>
    /// 相册管理
    /// </summary>
    public interface IPictureSAppService : IApplicationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PageResult<PicturesPageOutput>> GetPage([FromBody] PicturesPageQueryInput dto);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task AddPictures(AddPictureInput dto);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task Delete(KeyDto dto);

    }
}
