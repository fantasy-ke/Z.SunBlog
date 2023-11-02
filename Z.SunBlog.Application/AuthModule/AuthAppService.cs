using Microsoft.AspNetCore.Authentication;
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
using Z.SunBlog.Application.FriendLinkModule.BlogServer;
using Z.SunBlog.Core.CustomConfigModule;
using Z.Ddd.Common.Entities.Enum;
using Z.Module.DependencyInjection;
using Z.SunBlog.Application.UserModule.Dto;
using Lazy.Captcha.Core;
using Z.Ddd.Common.Attributes;
using Z.SunBlog.Core.UserModule.DomainManager;
using Z.Ddd.Common.Extensions;
using Azure;

namespace Z.SunBlog.Application.OAuthModule
{
    public interface IAuthAppService : IApplicationService, ITransientDependency
    {
        Task SignIn(ZUserInfoDto dto);

        IActionResult Captcha([FromQuery] string id);
    }
    /// <summary>
    /// 第三方登陆
    /// </summary>
    public class AuthAppService : ApplicationService, IAuthAppService
    {
        private readonly ICustomConfigAppService _customConfigService;
        private readonly IIdGenerator _idGenerator;
        private readonly IJwtTokenProvider _jwtTokenProvider;
        private readonly ICacheManager _cacheManager;
        private readonly ICaptcha _captcha;
        private readonly IUserDomainManager _userDomainManager;
        public AuthAppService(
            IServiceProvider serviceProvider,
            ICustomConfigAppService customConfigService,
            IIdGenerator idGenerator, IJwtTokenProvider jwtTokenProvider,
            IHttpContextAccessor httpContextAccessor,
            ICacheManager cacheManager, ICaptcha captcha, IUserDomainManager userDomainManager) : base(serviceProvider)
        {
            _customConfigService = customConfigService;
            _idGenerator = idGenerator;
            _jwtTokenProvider = jwtTokenProvider;
            _cacheManager = cacheManager;
            _captcha = captcha;
            _userDomainManager = userDomainManager;
        }

        /// <summary>
        /// 系统用户登录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task SignIn(ZUserInfoDto dto)
        {
            bool validate = _captcha.Validate(dto.Id, dto.Code);
            if (!validate)
            {
                throw new UserFriendlyException("验证码错误");
            }

            string signInErrorCacheKey = $"login.error.{dto.UserName}";
            var value = await _cacheManager.GetCacheAsync<int>(signInErrorCacheKey);
            var setting = await _customConfigService.Get<SysSecuritySetting>();
            //5分钟内连续验证密码失败超过4次将限制用户尝试
            if (value > (setting?.Times ?? 4))
            {
                throw new UserFriendlyException("由于您多次登录失败，系统已限制账户登录");
            }
            var user = await _userDomainManager.QueryAsNoTracking.FirstOrDefaultAsync(x => x.UserName == dto.UserName);
            if (user == null)
            {
                throw new UserFriendlyException("用户名或密码错误");
            }

            if (user.Status == AvailabilityStatus.Disable || (user.LockExpired.HasValue && DateTime.Now < user.LockExpired))
            {
                throw new UserFriendlyException("您的账号被锁定");
            }

            if (user.PassWord != MD5Encryption.Encrypt($"{_idGenerator.Encode(user.Id)}{dto.PassWord}"))
            {
                await _cacheManager.SetCacheAsync(signInErrorCacheKey, value + 1, TimeSpan.FromMinutes(5));
                throw new UserFriendlyException("用户名或密码错误");
            }
            UserTokenModel tokenModel = new UserTokenModel();
            tokenModel.UserName = user.UserName!;
            tokenModel.UserId = user.Id!;
            //.GetSection("App:JWtSetting").Get<JwtSettings>()
            var tokenConfig = AppSettings.AppOption<JwtSettings>("App:JWtSetting");
            var token = _jwtTokenProvider.GenerateAccessToken(tokenModel);

            App.HttpContext.Response.Cookies.Append("access-token", token, new CookieOptions()
            {
                Expires = DateTimeOffset.UtcNow.AddMinutes(tokenConfig.AccessTokenExpirationMinutes)
            });

            var claimsIdentity = new ClaimsIdentity(tokenModel.Claims, "Login");

            AuthenticationProperties properties = new AuthenticationProperties();
            properties.AllowRefresh = true;
            properties.IsPersistent = true;
            properties.IssuedUtc = DateTimeOffset.UtcNow;
            properties.ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1);
            await App.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="id">验证码唯一id</param>
        /// <returns></returns>
        [HttpGet]
        [NoResult]
        public IActionResult Captcha([FromQuery] string id)
        {
            var data = _captcha.Generate(id);
            var stream = new MemoryStream(data.Bytes);
            return new FileStreamResult(stream, "image/gif");
        }

    }
}
