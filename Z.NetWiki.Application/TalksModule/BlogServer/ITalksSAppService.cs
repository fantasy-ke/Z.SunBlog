using Microsoft.AspNetCore.Mvc;
using Z.Ddd.Common.DomainServiceRegister;
using Z.Ddd.Common.ResultResponse;
using Z.NetWiki.Application.TalksModule.BlogServer.Dto;

namespace Z.NetWiki.Application.TalksModule.BlogServer
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
