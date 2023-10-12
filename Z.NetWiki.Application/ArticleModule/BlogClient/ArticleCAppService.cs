using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using System.Linq.Dynamic.Core;
using Z.Ddd.Common.DomainServiceRegister;
using Z.Ddd.Common.Exceptions;
using Z.Ddd.Common.ResultResponse;
using Z.EntityFrameworkCore;
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
using Z.NetWiki.Domain.PraiseModule;
using Z.NetWiki.Domain.PraiseModule.DomainManager;
using Z.NetWiki.Domain.SharedDto;
using Z.NetWiki.Domain.TagModule;
using Z.NetWiki.Domain.TagsModule.DomainManager;

namespace Z.NetWiki.Application.ArticleModule.BlogServer
{
    /// <summary>
    /// 文章后台管理
    /// </summary>
    public class ArticleCAppService : ApplicationService, IArticleCAppService
    {
        private readonly IArticleDomainManager _articleDomainManager;
        private readonly IArticleTagManager _articleTagManager;
        private readonly IArticleCategoryManager _articleCategoryManager;
        private readonly ICategoriesManager _categoriesManager;
        private readonly ITagsManager _tagsManager;
        private readonly IPraiseManager _praiseManager;
        public ArticleCAppService(
            IServiceProvider serviceProvider, IArticleDomainManager articleDomainManager,
            IArticleTagManager articleTagManager, IArticleCategoryManager articleCategoryManager, ICategoriesManager categoriesManager, ITagsManager tagsManager, IPraiseManager praiseManager) : base(serviceProvider)
        {
            _articleDomainManager = articleDomainManager;
            _articleTagManager = articleTagManager;
            _articleCategoryManager = articleCategoryManager;
            _categoriesManager = categoriesManager;
            _tagsManager = tagsManager;
            _praiseManager = praiseManager;
        }

        /// <summary>
        /// 文章栏目分类
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<CategoryOutput>> Categories()
        {
            var queryable = _articleDomainManager.QueryAsNoTracking.Where(a => a.Status == AvailabilityStatus.Enable && a.PublishTime <= DateTime.Now && (a.ExpiredTime == null || DateTime.Now < a.ExpiredTime));

            return await _categoriesManager.QueryAsNoTracking.LeftJoin(
                _articleCategoryManager.QueryAsNoTracking,
                c => c.Id,
                ac => ac.CategoryId,
                (c, ac) => new { categories = c, articleCate = ac })
                .LeftJoin(
                queryable,
                ac => ac.articleCate.Id,
                qa => qa.Id,
                (c, qa) => new { categories = c.categories, articleCate = c.articleCate, queryable = qa })
                .Where(c => c.categories.Status == AvailabilityStatus.Enable)
                .GroupBy(c => new { c.categories.Id, c.categories.ParentId, c.categories.Name, c.categories.Sort })
                  .Select(c => new CategoryOutput
                  {
                      Id = c.Key.Id,
                      ParentId = c.Key.ParentId,
                      Sort = c.Key.Sort,
                      Name = c.Key.Name,
                      Total = c.Count()
                  })
                .OrderBy(x => x.Sort)
                .OrderBy(x => x.ParentId)
                .OrderBy(x => x.Id)
                .ToListAsync();
        }

        [HttpGet]
        public Task<PageResult<ArticleOutput>> GetList([FromQuery] ArticleListQueryInput dto)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 文章详情
        /// </summary>
        /// <param name="id">文章ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ArticleInfoOutput> Info([FromQuery] Guid id)
        {

            var article = await (from a in _articleDomainManager.QueryAsNoTracking
                        .Where(x => (x.Id == id && x.PublishTime <= DateTime.Now && x.Status == AvailabilityStatus.Enable) ||( x.ExpiredTime == null || x.ExpiredTime > DateTime.Now))
                        join ac in _articleCategoryManager.QueryAsNoTracking on
                        a.Id equals ac.ArticleId into category
                        from c in category.DefaultIfEmpty()

                        join cg in _categoriesManager.QueryAsNoTracking.Where(x=>x.Status == AvailabilityStatus.Enable) on
                        c.CategoryId equals cg.Id 

                        select new ArticleInfoOutput
                        {
                            Id = a.Id,
                            Title = a.Title,
                            Content = a.Content,
                            Summary = a.Summary,
                            Cover = a.Cover,
                            PublishTime = a.PublishTime,
                            Author = a.Author,
                            Views = a.Views,
                            CreationType = a.CreationType,
                            IsAllowComments = a.IsAllowComments,
                            IsHtml = a.IsHtml,
                            IsTop = a.IsTop,
                            Link = a.Link,
                            UpdatedTime = a.UpdatedTime,
                            CategoryId = c.Id,
                            PraiseTotal = _praiseManager.QueryAsNoTracking.Count(p => p.ObjectId == a.Id),
                            //IsPraise = SqlFunc.Subqueryable<Praise>().Where(p => p.ObjectId == a.Id).Any(),
                            CategoryName = cg.Name
                        }).FirstOrDefaultAsync();
            var tagsList = await (from t in _tagsManager.QueryAsNoTracking.Where(p=>p.Status == AvailabilityStatus.Enable)
                        join at in _articleTagManager.QueryAsNoTracking.Where(p => p.ArticleId == id)
                        on t.Id equals at.TagId
                        select new TagsOutput
                        {
                            Id = t.Id,
                            Name = t.Name,
                            Color = t.Color,
                            Icon = t.Icon
                        }).ToListAsync();



            article.Tags = tagsList;


            if (article == null) throw new UserFriendlyException("糟糕，您访问的信息丢失了...",404);

            var updateArt = await _articleDomainManager.FindByIdAsync(article.Id);

            updateArt.Views += 1;

            await _articleDomainManager.Update(updateArt);

            
            //上一篇
            var prevQuery = _articleDomainManager.QueryAsNoTracking.Where(x => x.PublishTime < article.PublishTime && x.PublishTime <= DateTime.Now && x.Status == AvailabilityStatus.Enable)
                .Where(x => x.ExpiredTime == null || x.ExpiredTime > DateTime.Now)
                 .OrderByDescending(x => x.PublishTime)
                 .Select(x => new ArticleBasicsOutput { Id = x.Id, Cover = x.Cover, Title = x.Title, PublishTime = null, Type = 0 }).Take(1);
            //下一篇
            var nextQuery = _articleDomainManager.QueryAsNoTracking.Where(x => x.PublishTime > article.PublishTime && x.PublishTime <= DateTime.Now && x.Status == AvailabilityStatus.Enable)
                .Where(x => x.ExpiredTime == null || x.ExpiredTime > DateTime.Now)
                    .OrderBy(x => x.PublishTime)
                    .Select(x => new ArticleBasicsOutput { Id = x.Id, Cover = x.Cover, Title = x.Title, PublishTime = null, Type = 1 }).Take(1);
            prevQuery.Union(nextQuery);

            //随机6条
            var randomQuery = _articleDomainManager.QueryAsNoTracking.Where(x => x.Id != id)
                .Where(x => x.PublishTime <= DateTime.Now && x.Status == AvailabilityStatus.Enable)
                .Where(x => x.ExpiredTime == null || x.ExpiredTime > DateTime.Now)
                .OrderBy(x => x.CreationTime)
                .Select(x => new ArticleBasicsOutput
                { Id = x.Id, Cover = x.Cover, Title = x.Title, PublishTime = x.PublishTime, Type = 2 })
                .Take(6);
            prevQuery.Union(randomQuery);


            //相关文章
            var relevant = await prevQuery.ToListAsync();
            article.Prev = relevant.FirstOrDefault(x => x.Type == 0);
            article.Next = relevant.FirstOrDefault(x => x.Type == 1);
            article.Random = relevant.Where(x => x.Type == 2).ToList();
            article.Views++;
            return article;
        }

        /// <summary>
        /// 最新5片文章
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<ArticleBasicsOutput>> Latest()
        {
            var result = await _articleDomainManager.QueryAsNoTracking
              .Where(x => x.Status == AvailabilityStatus.Enable && x.PublishTime <= DateTime.Now)
              .Where(x => x.ExpiredTime == null || x.ExpiredTime > DateTime.Now)
              .Take(5)
              .OrderByDescending(x => x.Id)
              .ToListAsync();


            return ObjectMapper.Map<List<ArticleBasicsOutput>>(result);
        }

        [HttpGet]
        public Task<ArticleReportOutput> Report()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public async Task<List<TagsOutput>> Tags()
        {
            return await _tagsManager.QueryAsNoTracking.Where(x => x.Status == AvailabilityStatus.Enable)
                .OrderBy(x => x.Sort)
                .Select(x => new TagsOutput
                {
                    Id = x.Id,
                    Icon = x.Icon,
                    Name = x.Name,
                    Color = x.Color
                }).ToListAsync();
        }
    }


}
