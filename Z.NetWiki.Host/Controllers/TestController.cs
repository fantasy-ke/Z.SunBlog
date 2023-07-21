using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Z.Ddd.Domain.Authorization;
using Z.Ddd.Domain.UserSession;
using Microsoft.AspNetCore.Authorization;

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
        private readonly IAuthorizationMiddlewareResultHandler _authorizationMiddlewareResultHandler;
        public TestController(IJwtTokenProvider jwtTokenProvider, IUserSession userSession, IAuthorizationMiddlewareResultHandler authorizationMiddlewareResultHandler)
        {
            _jwtTokenProvider = jwtTokenProvider;
            _userSession = userSession;
            _authorizationMiddlewareResultHandler = authorizationMiddlewareResultHandler;
        }

        /// <summary>
        /// 获取jwttoken
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> Login()
        {
            UserTokenModel tokenModel = new UserTokenModel();
            tokenModel.UserName = "test";
            tokenModel.UserId = Guid.NewGuid().ToString("N");
            var token = _jwtTokenProvider.GenerateAccessToken(tokenModel);

            Response.Cookies.Append("x-access-token", token);
            var claimsIdentity = new ClaimsIdentity(tokenModel.Claims, "Login");

            AuthenticationProperties properties = new AuthenticationProperties();
            properties.AllowRefresh = false;
            properties.IsPersistent = true;
            properties.IssuedUtc = DateTimeOffset.UtcNow;
            properties.ExpiresUtc = DateTimeOffset.Now.AddMinutes(1);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);

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
    }
}
