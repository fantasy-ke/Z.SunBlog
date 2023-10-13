using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Z.Ddd.Common.DomainServiceRegister;
using Z.Ddd.Common.ResultResponse;
using Z.Ddd.Common.UserSession;
using Z.EntityFrameworkCore.Extensions;
using Z.NetWiki.Application.AlbumsModule.BlogServer.Dto;
using Z.NetWiki.Application.ArticleModule.BlogClient.Dto;
using Z.NetWiki.Application.ArticleModule.BlogServer.Dto;
using Z.NetWiki.Application.TalksModule.BlogServer.Dto;
using Z.NetWiki.Domain.AlbumsModule;
using Z.NetWiki.Domain.AlbumsModule.DomainManager;
using Z.NetWiki.Domain.ArticleCategoryModule;
using Z.NetWiki.Domain.ArticleCategoryModule.DomainManager;
using Z.NetWiki.Domain.ArticleModule;
using Z.NetWiki.Domain.ArticleModule.DomainManager;
using Z.NetWiki.Domain.ArticleTagModule;
using Z.NetWiki.Domain.ArticleTagModule.DomainManager;
using Z.NetWiki.Domain.CategoriesModule;
using Z.NetWiki.Domain.CategoriesModule.DomainManager;
using Z.NetWiki.Domain.Enum;
using Z.NetWiki.Domain.PicturesModule.DomainManager;
using Z.NetWiki.Domain.PraiseModule;
using Z.NetWiki.Domain.PraiseModule.DomainManager;
using Z.NetWiki.Domain.SharedDto;
using Z.NetWiki.Domain.TagModule;
using Z.NetWiki.Domain.TagsModule.DomainManager;
using Z.NetWiki.Domain.TalksModule;
using Z.NetWiki.Domain.TalksModule.DomainManager;

namespace Z.NetWiki.Application.TalksModule.BlogServer
{
    /// <summary>
    /// 说说管理
    /// </summary>
    public class TalksSAppService : ApplicationService, ITalksSAppService
    {
        private readonly ITalksManager _talksManager;
        private readonly IUserSession _userSession;
        private readonly IPraiseManager _praiseManager;
        public TalksSAppService(
            IServiceProvider serviceProvider, ITalksManager talksManager, IUserSession userSession, IPraiseManager praiseManager) : base(serviceProvider)
        {
            _talksManager = talksManager;
            _userSession = userSession;
            _praiseManager = praiseManager;
        }

        /// <summary>
        /// 添加修改说说
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task CreateOrUpdate(CreateOrUpdateTalksInput dto)
        {
            if (dto.Id != null && dto.Id != Guid.Empty)
            {
                await Update(dto);
            }

            await Create(dto);
        }

        /// <summary>
        /// 说说分页查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<TalksPageOutput>> GetPage([FromBody] TalksPageQueryInput dto)
        {
            string userId = _userSession.UserId;
            return await _talksManager.QueryAsNoTracking
                  .WhereIf(!string.IsNullOrWhiteSpace(dto.Keyword), x => x.Content.Contains(dto.Keyword))
                  .OrderByDescending(x => x.Id)
                  .Select(x => new TalksPageOutput
                  {
                      Id = x.Id,
                      Status = x.Status,
                      Content = x.Content,
                      Images = x.Images,
                      IsAllowComments = x.IsAllowComments,
                      IsTop = x.IsTop,
                      IsPraise = _praiseManager.QueryAsNoTracking.Where(p => p.ObjectId == x.Id && p.AccountId == userId).Any(),
                      CreatedTime = x.CreationTime
                  }).ToPagedListAsync(dto);
        }





        /// <summary>
        /// 添加说说
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private async Task Create(CreateOrUpdateTalksInput dto)
        {
            var talks = ObjectMapper.Map<Talks>(dto);
            await _talksManager.Create(talks);
        }

        /// <summary>
        /// 更新说说
        /// </summary>
        /// <returns></returns>
        private async Task Update(CreateOrUpdateTalksInput dto)
        {
            var talks = await _talksManager.FindByIdAsync(dto.Id!.Value);

            ObjectMapper.Map(dto, talks);

            await _talksManager.Update(talks!);
        }
    }


}
