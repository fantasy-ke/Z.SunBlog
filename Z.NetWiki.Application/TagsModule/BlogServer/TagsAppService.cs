using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Z.Ddd.Common.DomainServiceRegister;
using Z.Ddd.Common.ResultResponse;
using Z.EntityFrameworkCore.Extensions;
using Z.NetWiki.Application.TagsModule.BlogServer.Dto;
using Z.NetWiki.Domain.Enum;
using Z.NetWiki.Domain.SharedDto;
using Z.NetWiki.Domain.TagModule;
using Z.NetWiki.Domain.TagsModule.DomainManager;

namespace Z.NetWiki.Application.TagsModule.BlogServer
{
    /// <summary>
    /// 标签管理
    /// </summary>
    public class TagsAppService : ApplicationService, ITagsAppService
    {
        private readonly ITagsManager _tagsManager;
        public TagsAppService(
            IServiceProvider serviceProvider, ITagsManager tagsManager) : base(serviceProvider)
        {
            _tagsManager = tagsManager;
        }

        /// <summary>
        /// 添加修改
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task CreateOrUpdate(CreateOrUpdateTagInput dto)
        {
            if (dto.Id != null && dto.Id != Guid.Empty)
            {
                await Update(dto);
            }

            await Create(dto);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task DeleteAsync(KeyDto dto)
        {
            await _tagsManager.Delete(dto.Id);
        }


        /// <summary>
        /// 文章标签下拉选项
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<SelectOutput>> Select()
        {
            return await _tagsManager.QueryAsNoTracking.Where(x => x.Status == AvailabilityStatus.Enable)
                  .OrderBy(x => x.Sort)
                  .Select(x => new SelectOutput()
                  {
                      Value = x.Id,
                      Label = x.Name
                  })
                  .ToListAsync();
        }

        /// <summary>
        /// 标签列表分页查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PageResult<TagsPageOutput>> GetPage([FromQuery] TagsPageQueryInput dto)
        {

            var query = await _tagsManager.QueryAsNoTracking
            .WhereIf(!string.IsNullOrWhiteSpace(dto.Name), x => x.Name.Contains(dto.Name))
            .OrderBy(x => x.Sort)
            .OrderByDescending(x => x.Id)
            .Select(x => new TagsPageOutput
            {
                Id = x.Id,
                Name = x.Name,
                Status = x.Status,
                Sort = x.Sort,
                Cover = x.Cover,
                CreatedTime = x.CreatedTime,
                Color = x.Color
            }).ToPagedListAsync(dto);

            return query;
        }

        /// <summary>
        /// 添加标签
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private async Task Create(CreateOrUpdateTagInput dto)
        {
            var tags = ObjectMapper.Map<Tags>(dto);
            tags.Id = Guid.NewGuid();

            await _tagsManager.Create(tags);
        }

        /// <summary>
        /// 更新标签
        /// </summary>
        /// <returns></returns>
        private async Task Update(CreateOrUpdateTagInput dto)
        {
            var tags = await _tagsManager.FindByIdAsync(dto.Id!.Value);

            ObjectMapper.Map(dto, tags);

            await _tagsManager.Update(tags!);
        }
    }


}
