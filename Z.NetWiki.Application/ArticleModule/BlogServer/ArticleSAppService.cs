﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Z.Ddd.Common.DomainServiceRegister;
using Z.Ddd.Common.ResultResponse;
using Z.EntityFrameworkCore.Extensions;
using Z.NetWiki.Application.ArticleModule.BlogClient.Dto;
using Z.NetWiki.Application.ArticleModule.BlogServer.Dto;
using Z.NetWiki.Domain.ArticleCategoryModule;
using Z.NetWiki.Domain.ArticleCategoryModule.DomainManager;
using Z.NetWiki.Domain.ArticleModule;
using Z.NetWiki.Domain.ArticleModule.DomainManager;
using Z.NetWiki.Domain.ArticleTagModule;
using Z.NetWiki.Domain.ArticleTagModule.DomainManager;
using Z.NetWiki.Domain.CategoriesModule;
using Z.NetWiki.Domain.CategoriesModule.DomainManager;
using Z.NetWiki.Domain.Enum;
using Z.NetWiki.Domain.SharedDto;
using Z.NetWiki.Domain.TagModule;
using Z.NetWiki.Domain.TagsModule.DomainManager;

namespace Z.NetWiki.Application.ArticleModule.BlogServer
{
    /// <summary>
    /// 文章后台管理
    /// </summary>
    public class ArticleSAppService : ApplicationService, IArticleSAppService
    {
        private readonly IArticleDomainManager _articleDomainManager;
        private readonly IArticleTagManager _articleTagManager;
        private readonly IArticleCategoryManager _articleCategoryManager;
        private readonly ICategoriesManager _categoriesManager;
        private readonly ITagsManager _tagsManager;
        public ArticleSAppService(
            IServiceProvider serviceProvider, IArticleDomainManager articleDomainManager,
            IArticleTagManager articleTagManager, IArticleCategoryManager articleCategoryManager, ICategoriesManager categoriesManager, ITagsManager tagsManager) : base(serviceProvider)
        {
            _articleDomainManager = articleDomainManager;
            _articleTagManager = articleTagManager;
            _articleCategoryManager = articleCategoryManager;
            _categoriesManager = categoriesManager;
            _tagsManager = tagsManager;
        }

        /// <summary>
        /// 添加修改
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task CreateOrUpdate(CreateOrUpdateArticleInput dto)
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
        [HttpPost]
        public async Task DeleteAsync(KeyDto dto)
        {
            await _articleDomainManager.Delete(dto.Id);
        }

        /// <summary>
        /// 查询详细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public async Task<ArticleDetailOutput> GetDetail([FromQuery] Guid id)
        {
            var articles = await (from article in _articleDomainManager.QueryAsNoTracking
                        .Where(x => x.Id == id)
                                join ac in _articleCategoryManager.QueryAsNoTracking on
                                article.Id equals ac.ArticleId into category
                                from c in category.DefaultIfEmpty()

                                join cg in _categoriesManager.QueryAsNoTracking.Where(x => x.Status == AvailabilityStatus.Enable) on
                                c.CategoryId equals cg.Id

                                select new ArticleDetailOutput
                                {
                                    Id = article.Id,
                                    Title = article.Title,
                                    Summary = article.Summary,
                                    Cover = article.Cover,
                                    Status = article.Status,
                                    Link = article.Link,
                                    IsTop = article.IsTop,
                                    Sort = article.Sort,
                                    Author = article.Author,
                                    Content = article.Content,
                                    IsAllowComments = article.IsAllowComments,
                                    IsHtml = article.IsHtml,
                                    CreationType = article.CreationType,
                                    CategoryId = c.Id,
                                    ExpiredTime = article.ExpiredTime,
                                    PublishTime = article.PublishTime,
                                }).FirstOrDefaultAsync();

            var tagsList = await(from t in _tagsManager.QueryAsNoTracking.Where(p => p.Status == AvailabilityStatus.Enable)
                                 join at in _articleTagManager.QueryAsNoTracking.Where(p => p.ArticleId == id)
                                 on t.Id equals at.TagId
                                 select t.Id).ToListAsync();



            articles!.Tags = tagsList;


            return articles;
        }

        /// <summary>
        /// 文章列表分页查询
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<ArticlePageOutput>> GetPage([FromBody] ArticlePageQueryInput dto)
        {
            List<Guid> categoryList = new();
            if (dto.CategoryId.HasValue)
            {
                //var list = await _categoriesManager.QueryAsNoTracking
                //    .Where(x => x.Status == AvailabilityStatus.Enable)
                //    .ToChildListAsync(x => x.ParentId, dto.CategoryId);

                categoryList = await _categoriesManager.QueryAsNoTracking
                    .Where(x => x.ParentId == dto.CategoryId
                && x.Status == AvailabilityStatus.Enable).Select(p => p.Id).ToListAsync();
                categoryList.Add(dto.CategoryId.Value);
            }

            var query = await _articleDomainManager.QueryAsNoTracking.LeftJoin(_articleCategoryManager.QueryAsNoTracking,
                a => a.Id,
                ac => ac.ArticleId,
                (a, ac) => new { article = a, articleCategory = ac })
                .Join(_categoriesManager.QueryAsNoTracking,
                c => c.articleCategory.Id,
                ca => ca.Id,
                (c, ca) => new
                {
                    categories = ca,
                    c.article,
                    c.articleCategory
                }
                ).WhereIf(!string.IsNullOrWhiteSpace(dto.Title), p => p.article.Title.Contains(dto.Title) || p.article.Summary.Contains(dto.Title) || p.article.Content.Contains(dto.Title))
                .WhereIf(categoryList.Any(), p => categoryList.Contains(p.articleCategory.CategoryId))
                .OrderByDescending(p => p.article.IsTop)
                  .OrderBy(p => p.article.Sort)
                  .OrderByDescending(p => p.article.PublishTime)
                   .Select((a) => new ArticlePageOutput
                   {
                       Id = a.article.Id,
                       Title = a.article.Title,
                       Status = a.article.Status,
                       Sort = a.article.Sort,
                       Cover = a.article.Cover,
                       IsTop = a.article.IsTop,
                       CreationType = a.article.CreationType,
                       PublishTime = a.article.PublishTime,
                       Views = a.article.Views,
                       CategoryName = a.categories.Name
                   }).ToPagedListAsync(dto);


            return query;
        }

        /// <summary>
        /// 添加文章
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private async Task Create(CreateOrUpdateArticleInput dto)
        {
            var article = ObjectMapper.Map<Article>(dto);
            article.Id = Guid.NewGuid();
            var tags = dto.Tags.Select(x => new ArticleTag()
            {
                ArticleId = article.Id,
                TagId = x
            }).ToList();
            await _articleDomainManager.Create(article);
            await _articleTagManager.Create(tags);
            var articleCategory = new ArticleCategory()
            {
                ArticleId = article.Id,
                CategoryId = dto.CategoryId
            };
            await _articleCategoryManager.Create(articleCategory);
        }

        /// <summary>
        /// 更新文章
        /// </summary>
        /// <returns></returns>
        private async Task Update(CreateOrUpdateArticleInput dto)
        {
            var article = await _articleDomainManager.QueryAsNoTracking.FirstOrDefaultAsync(p => p.Id == dto.Id);

            var articleEntity = ObjectMapper.Map<Article>(dto);

            await _articleDomainManager.Update(articleEntity);


            await _articleTagManager.Delete(p => p.ArticleId == dto.Id);
            var tags = dto.Tags.Select(x => new ArticleTag()
            {
                ArticleId = articleEntity.Id,
                TagId = x
            }).ToList();

            await _articleTagManager.Create(tags);

            var cate = await _articleCategoryManager.QueryAsNoTracking.FirstOrDefaultAsync(x => x.ArticleId == dto.Id);
            cate.CategoryId = dto.CategoryId;
            await _articleCategoryManager.Update(cate);
        }
    }


}
