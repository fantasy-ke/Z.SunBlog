using Microsoft.AspNetCore.Mvc;
using Z.Ddd.Common.DomainServiceRegister;
using Z.Ddd.Common.ResultResponse;
using Z.SunBlog.Application.TalksModule.BlogServer.Dto;

namespace Z.SunBlog.Application.TalksModule.BlogServer
{
    /// <summary>
    /// 相册管理
    /// </summary>
    public interface ITalksSAppService : IApplicationService
    {
        Task<PageResult<TalksPageOutput>> GetPage([FromBody] TalksPageQueryInput dto);

        Task CreateOrUpdate(CreateOrUpdateTalksInput dto);

    }
}
