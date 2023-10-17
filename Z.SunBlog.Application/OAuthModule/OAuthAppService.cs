using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MrHuo.OAuth.QQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Z.Ddd.Common.DomainServiceRegister;
using Z.Ddd.Common.UserSession;
using Z.SunBlog.Application.TagsModule.BlogServer;
using Z.SunBlog.Core.AuthAccountModule;
using Z.SunBlog.Core.AuthAccountModule.DomainManager;
using Z.SunBlog.Core.FriendLinkModule.DomainManager;
using Z.SunBlog.Core.TagsModule.DomainManager;

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
        private readonly IUserSession _userSession;
        private readonly IAuthAccountDomainManager _authAccountDomainManager;
        private readonly IFriendLinkManager _friendLinkManager;
        public OAuthAppService(
            IServiceProvider serviceProvider, QQOAuth qqoAuth, IUserSession userSession, IAuthAccountDomainManager authAccountDomainManager, IFriendLinkManager friendLinkManager) : base(serviceProvider)
        {
            _qqoAuth = qqoAuth;
            _userSession = userSession;
            _authAccountDomainManager = authAccountDomainManager;
            _friendLinkManager = friendLinkManager;
        }

        /// <summary>
        /// 获取授权地址
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        //public async Task<string> Get(string type)
        //{
        //    string code = _idGenerator.Encode(_idGenerator.NewLong());
        //    var referer = _httpContextAccessor.HttpContext!.Request.Headers.FirstOrDefault(x => x.Key.Equals("Referer", StringComparison.CurrentCultureIgnoreCase)).Value;
        //    await _easyCachingProvider.SetAsync($"{OAuthRedirectKey}{code}", referer, TimeSpan.FromMinutes(5));
        //    string url = type.ToLower() switch
        //    {
        //        "qq" => _qqoAuth.GetAuthorizeUrl(code),
        //        _ => throw Oops.Bah("无效请求")
        //    };
        //    return url;
        //}
    }
}
