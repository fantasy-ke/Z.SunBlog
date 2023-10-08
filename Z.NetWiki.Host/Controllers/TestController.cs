using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Z.Ddd.Common.Authorization;
using Z.Ddd.Common.UserSession;
using Microsoft.AspNetCore.Authorization;
using Z.Ddd.Common.Entities.IAuditing;
using Z.Ddd.Common.Entities.Auditing;
using Z.NetWiki.Application.UserModule;
using Z.Ddd.Common.Entities.Users;
using Serilog;
using Z.Ddd.Common.RedisModule;
using Z.NetWiki.Application.UserModule.Dto;
using Z.Ddd.Common.Exceptions;

namespace Z.NetWiki.Host.Controllers
{
    /// <summary>
    /// 测试控制器
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IJwtTokenProvider _jwtTokenProvider;
        private readonly IUserSession _userSession;
        private readonly IUserAppService _userAppService;
        private ICacheManager _cacheManager;
        public TestController(IJwtTokenProvider jwtTokenProvider, IUserSession userSession, IUserAppService userAppService, ICacheManager cacheManager)
        {
            _jwtTokenProvider = jwtTokenProvider;
            _userSession = userSession;
            _userAppService = userAppService;
            _cacheManager = cacheManager;
        }





        /// <summary>
        /// 获取jwttoken
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> Login(ZUserInfoDto user)
        {
            UserTokenModel tokenModel = new UserTokenModel();
            var userinfo = await _userAppService.Login(user);
            if (userinfo == null)
            {
                throw new UserFriendlyException("账号密码错误");

            }
            tokenModel.UserName = userinfo.UserName!;
            tokenModel.UserId = userinfo.Id!;
            var token = _jwtTokenProvider.GenerateAccessToken(tokenModel);

            Response.Cookies.Append("x-access-token", token, new CookieOptions()
            {
                Expires = DateTimeOffset.UtcNow.AddMinutes(20)
        });
            var claimsIdentity = new ClaimsIdentity(tokenModel.Claims, "Login");

            AuthenticationProperties properties = new AuthenticationProperties();
            properties.AllowRefresh = true;
            properties.IsPersistent = true;
            properties.IssuedUtc = DateTimeOffset.UtcNow;
            properties.ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(20);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);

            Log.Logger.Information("登录成功");
            return token;
        }


        /// <summary>
        /// 获取jwttoken
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ZAuthorization]
        public async Task<string> GetUser()
        {
            var user = _userSession.UserName;
            var userid = _userSession.UserId;

            return user;
        }

        [HttpGet]
        public async Task<string> Logout()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return "ddd";
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ZAuthorization]
        public async Task<JsonResult> CreateUser()
        {
            return new JsonResult(await _userAppService.Create());
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ZAuthorization]
        public async Task<List<ZUserInfoDto>> SeacthUser()
        {

            var user = await _userAppService.GetFrist();

            await _cacheManager.SetCacheAsync("user1", user);

            return user;
        }


        /// <summary>
        /// 获取用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ZAuthorization]
        public async Task<List<ZUserInfoDto>> SeacthUserCache()
        {

            var user = await _cacheManager.GetCacheAsync<List<ZUserInfoDto>>("user1");

            return user;
        }

    }
}
