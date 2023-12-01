﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Yitter.IdGenerator;
using Z.Ddd.Common;
using Z.Ddd.Common.Authorization;
using Z.Ddd.Common.DomainServiceRegister;
using Z.Ddd.Common.Exceptions;
using Z.Ddd.Common.RedisModule;
using Z.Ddd.Common.UserSession;
using Z.SunBlog.Application.OAuthModule.Dto;
using Z.SunBlog.Core.AlbumsModule.DomainManager;
using Z.SunBlog.Core.AuthAccountModule;
using Z.SunBlog.Core.AuthAccountModule.DomainManager;
using Z.SunBlog.Core.CustomConfigModule;
using Z.SunBlog.Core.FriendLinkModule;
using Z.SunBlog.Core.FriendLinkModule.DomainManager;
using Z.SunBlog.Core.PicturesModule.DomainManager;
using Z.Ddd.Common.Entities.Enum;
using Z.SunBlog.Application.ConfigModule;
using Serilog;
using MrHuo.OAuth;
using MrHuo.OAuth.QQ;
using MrHuo.OAuth.Gitee;
using Z.Ddd.Common.Attributes;
using Z.Ddd.Common.Authorization.Dtos;
using MrHuo.OAuth.Github;
using Z.Ddd.Common.Helper;

namespace Z.SunBlog.Application.OAuthModule
{
    /// <summary>
    /// 第三方登陆
    /// </summary>
    public class OAuthAppService : ApplicationService, IOAuthAppService
    {
        /// <summary>
        /// 第三方授权缓存
        /// </summary>
        private const string OAuthKey = "oauth.";
        /// <summary>
        /// 授权成功后回调页面缓存键
        /// </summary>
        private const string OAuthRedirectKey = "oauth.redirect.";
        private readonly QQOAuth _qqoAuth;
        private readonly GiteeOAuth _giteeoAuth;
        private readonly GithubOAuth _githuboAuth;
        private readonly IAuthAccountDomainManager _authAccountDomainManager;
        private readonly IFriendLinkManager _friendLinkManager;
        private readonly IIdGenerator _idGenerator;
        private readonly ICacheManager _cacheManager;
        private readonly ICustomConfigAppService _customConfigAppService;
        private readonly IJwtTokenProvider _jwtTokenProvider;
        private readonly IAlbumsManager _albumsManager;
        private readonly IPicturesManager _picturesManager;
        public OAuthAppService(
            IServiceProvider serviceProvider,
            QQOAuth qqoAuth,
            IAuthAccountDomainManager authAccountDomainManager,
            IFriendLinkManager friendLinkManager,
            IIdGenerator idGenerator,
            ICacheManager cacheManager,
            IJwtTokenProvider jwtTokenProvider,
            ICustomConfigAppService customConfigAppService,
            IAlbumsManager albumsManager,
            IPicturesManager picturesManager,
            GiteeOAuth giteeoAuth,
            GithubOAuth githuboAuth) : base(serviceProvider)
        {
            _qqoAuth = qqoAuth;
            _authAccountDomainManager = authAccountDomainManager;
            _friendLinkManager = friendLinkManager;
            _idGenerator = idGenerator;
            _cacheManager = cacheManager;
            _jwtTokenProvider = jwtTokenProvider;
            _customConfigAppService = customConfigAppService;
            _albumsManager = albumsManager;
            _picturesManager = picturesManager;
            _giteeoAuth = giteeoAuth;
            _githuboAuth = githuboAuth;
        }

        /// <summary>
        /// 获取授权地址
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> GetIpAddress(string type)
        {
            string code = Guid.NewGuid().ToString();
            Log.Warning("code内容", code);
            var referer = App.HttpContext!.Request.Headers.FirstOrDefault(x => x.Key.Equals("Referer", StringComparison.CurrentCultureIgnoreCase)).Value;
            Log.Warning("referer内容", referer);
            await _cacheManager.SetCacheAsync($"{OAuthRedirectKey}{code}", referer.FirstOrDefault().ToLower(), TimeSpan.FromMinutes(5));
            string url = type.ToLower() switch
            {
                "qq" => _qqoAuth.GetAuthorizeUrl(code),
                "gitee" => _giteeoAuth.GetAuthorizeUrl(code),
                "github" => _githuboAuth.GetAuthorizeUrl(code),
                _ => throw new UserFriendlyException("无效请求")
            };
            Log.Warning("url内容", url);
            return url;
        }

        /// <summary>
        /// 授权回调
        /// </summary>
        /// <param name="type">授权类型</param>
        /// <param name="code"></param>
        /// <param name="state">缓存唯一ID</param>
        /// <returns></returns>
        [HttpGet]
        [NoResult]
        public async Task<IActionResult> Callback(string type,[FromQuery] string code, [FromQuery] string state)
        {
            if (string.IsNullOrWhiteSpace(state) || !await _cacheManager.ExistsAsync($"{OAuthRedirectKey}{state}"))
            {
                throw new UserFriendlyException("缺少参数");
            }
            AuthAccount account;
            switch (type.ToLower())
            {
                case "qq":
                    var qqResult = await _qqoAuth.AuthorizeCallback(code, state);
                    if (!qqResult.IsSccess)
                    {
                        throw new UserFriendlyException(qqResult.ErrorMessage);
                    }
                    string openId = await _qqoAuth.GetOpenId(qqResult.AccessToken.AccessToken);
                    account = await _authAccountDomainManager.CreateQQAccount(qqResult.UserInfo, openId);
                    break;
                case "gitee":
                    var giteeResult = await _giteeoAuth.AuthorizeCallback(code, state);
                    if (!giteeResult.IsSccess)
                    {
                        throw new UserFriendlyException(giteeResult.ErrorMessage);
                    }
                    account = await _authAccountDomainManager.CreateGiteeAccount(giteeResult.UserInfo);
                    break;
                case "github":
                    var githubResult = await _githuboAuth.AuthorizeCallback(code, state);
                    if (!githubResult.IsSccess)
                    {
                        throw new UserFriendlyException(githubResult.ErrorMessage);
                    }
                    account = await _authAccountDomainManager.CreateGitHubAccount(githubResult.UserInfo);
                    break;
                default:
                    throw new UserFriendlyException("无效请求");
            }
            string key = $"{OAuthKey}{state}";
            await _cacheManager.SetCacheAsync(key, account, TimeSpan.FromSeconds(30));
            //登录成功后的回调页面
            string url = AppSettings.GetValue("oauth:redirect_uri")!;
            return new RedirectResult($"{url}?code={state}");
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ZFantasyToken> Login(string code)
        {
            string key = $"{OAuthKey}{code}", key2 = $"{OAuthRedirectKey}{code}";
            var value = await _cacheManager.GetCacheAsync<AuthAccount>(key);
            if (value == null)
            {
                throw new UserFriendlyException("无效参数");
            }
            var account = value!;
            var tokenConfig = AppSettings.AppOption<JwtSettings>("App:JWtSetting");
            // 设置Token的Claims
            List<Claim> claims = new List<Claim>
            {
                new Claim(ZClaimTypes.UserName, account.Name!), //HttpContext.User.Identity.Name
                new Claim(ZClaimTypes.UserId, account.Id!.ToString()),
                new Claim(ZClaimTypes.Expiration, DateTimeOffset.Now.AddMinutes(tokenConfig.AccessTokenExpirationMinutes).ToString()),
            };
            var token = _jwtTokenProvider.GenerateZToken(claims.ToArray());
            string url = await _cacheManager.GetCacheAsync<string>(key2);
            await _cacheManager.RemoveCacheAsync(key);
            await _cacheManager.RemoveCacheAsync(key2);
            token.RedirectUrl = url;
            return token;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        public async Task<OAuthAccountDetailOutput> UserInfo()
        {
            string id = UserService.UserId;
            var result=  await _authAccountDomainManager.QueryAsNoTracking
                .GroupJoin(_friendLinkManager.QueryAsNoTracking, ac => ac.Id, link => link.AppUserId, (ac, link) => new { ac = ac, link = link })
                .SelectMany(p => p.link.DefaultIfEmpty(), (ac, link) => new { ac = ac.ac, link = link })
                .Where(al => al.ac.Id == id)
                .Select(al => new OAuthAccountDetailOutput
                {
                    Id = al.ac.Id,
                    Avatar = al.ac.Avatar,
                    Status = al.link != null ? al.link.Status : null,
                    NickName = al.ac.Name,
                    Link = al.link != null ? al.link.Link : null,
                    Logo = al.link != null ? al.link.Logo : null,
                    SiteName = al.link != null ? al.link.SiteName : null,
                    Url = al.link != null ? al.link.Url : null,
                    Remark = al.link != null ? al.link.Remark : null,
                }).FirstOrDefaultAsync();

            return result;
        }

        /// <summary>
        /// 申请友链
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task AddLink(AddLinkOutput dto)
        {
            string userId = UserService.UserId;

            var link = await _friendLinkManager.QueryAsNoTracking.FirstAsync(x => x.AppUserId == userId);
            if (link == null)
            {
                link = ObjectMapper.Map<FriendLink>(dto);
                link.AppUserId = userId;
                link.Status = AvailabilityStatus.Disable;
                await _friendLinkManager.CreateAsync(link);
                return;
            }

            ObjectMapper.Map(dto, link);
            link.Status = AvailabilityStatus.Disable;
            await _friendLinkManager.UpdateAsync(link);
        }


        /// <summary>
        /// 博客基本信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<BlogOutput> Info()
        {
            var blogSetting = await _customConfigAppService.Get<BlogSetting>();
            blogSetting.WxPayUrl = blogSetting.WxPay.FirstOrDefault()?.Url;
            blogSetting.AliPayUrl = blogSetting.AliPay.FirstOrDefault()?.Url;
            blogSetting.LogoUrl = blogSetting.Logo.FirstOrDefault()?.Url;
            blogSetting.FaviconUrl = blogSetting.Favicon.FirstOrDefault()?.Url;
            var info = await _customConfigAppService.Get<BloggerInfo>();
            info.AvatarUrl = info.Avatar.FirstOrDefault()?.Url;
            var pics = await _albumsManager.QueryAsNoTracking
                .Join(_picturesManager.QueryAsNoTracking, a => a.Id, p => p.AlbumId, (a, p) => new { album = a, pic = p })
                .Where(p => p.album.Type.HasValue)
                .Select(p => new
                {
                    p.album.Type,
                    p.pic.Url
                }).ToListAsync();
            var dictionary = pics.GroupBy(x => x.Type).ToDictionary(x => x.Key!.ToString(), v => v.Select(x => x.Url).ToList());
            return new BlogOutput { Site = blogSetting, Info = info, Covers = dictionary };
        }

        /// <summary>
        /// 友情链接
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<FriendLinkOutput>> Links()
        {
            return await _friendLinkManager.QueryAsNoTracking.Where(x => x.Status == AvailabilityStatus.Enable)
                  .OrderBy(x => x.Sort)
                  .OrderBy(x => x.Id)
                  .Select(x => new FriendLinkOutput
                  {
                      Id = x.Id,
                      Link = x.Link,
                      Logo = x.Logo,
                      SiteName = x.SiteName,
                      Remark = x.Remark
                  }).ToListAsync();
        }
    }
}