using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.RegularExpressions;
using Z.Ddd.Common.DomainServiceRegister;
using Z.Ddd.Common.ResultResponse;
using Z.SunBlog.Application.Dto;
using Z.SunBlog.Application.FriendLinkModule.BlogServer.Dto;
using Z.SunBlog.Core.Const;
using Z.SunBlog.Core.CustomConfigModule;
using Z.SunBlog.Core.SharedDto;
using System.Collections;
using Z.Ddd.Common.RedisModule;
using Z.SunBlog.Core.CustomConfigModule.DomainManager;
using Microsoft.EntityFrameworkCore;

namespace Z.SunBlog.Application.FriendLinkModule.BlogServer
{
    public interface ICustomConfigAppService : IApplicationService
    {
        Task<T> Get<T>();
    }
    /// <summary>
    /// 文章管理
    /// </summary>
    public class CustomConfigAppService : ApplicationService, ICustomConfigAppService
    {
        private readonly ICacheManager _cacheManager;
        private readonly ICustomConfigManager _customConfigManager;
        private readonly ICustomConfigItemManager _customConfigItemManager;
        public CustomConfigAppService(IServiceProvider serviceProvider, ICacheManager cacheManager, ICustomConfigManager customConfigManager, ICustomConfigItemManager customConfigItemManager) : base(serviceProvider)
        {
            _cacheManager = cacheManager;
            _customConfigManager = customConfigManager;
            _customConfigItemManager = customConfigItemManager;
        }

        /// <summary>
        /// 获取强类型配置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [NonAction]
        public async Task<T> Get<T>()
        {
            var type = typeof(T);
            bool isList = typeof(IEnumerable).IsAssignableFrom(type);
            string code = type.IsGenericType && isList ? type.GenericTypeArguments[0].Name : type.Name;
            var value = await _cacheManager.GetCacheAsync($"{CacheConst.ConfigCacheKey}{code}", async () =>
            {
                var queryable = _customConfigManager.QueryAsNoTracking
                .Join(_customConfigItemManager.QueryAsNoTracking, c => c.Id, item => item.ConfigId, (c, item) => new { config = c, item = item })
                    .Where(c => c.config.Code == code)
                    .Select(c => c.item.Json);
                string json;
                if (isList)
                {
                    List<string> list = await queryable.ToListAsync();
                    if (!list.Any()) return default(T);
                    json = $"[{string.Join(",", list)}]";
                }
                else
                {
                    json = await queryable.FirstOrDefaultAsync();
                }

                return string.IsNullOrWhiteSpace(json) ? default(T) : JsonConvert.DeserializeObject<T>(json);
            }, TimeSpan.FromDays(1));

            return value;
        }
    }
}
