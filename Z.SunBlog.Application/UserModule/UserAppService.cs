using AutoMapper;
using Lazy.Captcha.Core;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Yitter.IdGenerator;
using Z.Ddd.Common;
using Z.Ddd.Common.Authorization;
using Z.Ddd.Common.DomainServiceRegister;
using Z.Ddd.Common.Entities.Users;
using Z.Ddd.Common.Exceptions;
using Z.Ddd.Common.Extensions;
using Z.Ddd.Common.RedisModule;
using Z.SunBlog.Application.FriendLinkModule.BlogServer;
using Z.SunBlog.Application.UserModule.Dto;
using Z.SunBlog.Core.CustomConfigModule;
using Z.SunBlog.Core.UserModule.DomainManager;
using Z.Ddd.Common.Attributes;
using System.ComponentModel;
using Z.Ddd.Common.UserSession;
using Z.Ddd.Common.Entities.Enum;

namespace Z.SunBlog.Application.UserModule
{
    /// <summary>
    /// 用户服务
    /// </summary>
	public class UserAppService : ApplicationService, IUserAppService
    {
        public readonly IUserDomainManager _userDomainManager;
        public readonly IUserSession _userSession;
        private readonly ICaptcha _captcha;
        private readonly ICustomConfigAppService _customConfigService;
        private readonly IIdGenerator _idGenerator;
        private readonly IJwtTokenProvider _jwtTokenProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICacheManager _cacheManager;
        public UserAppService(IUserDomainManager userDomainManager,
            IServiceProvider serviceProvider,
            ICaptcha captcha,
            IIdGenerator idGenerator,
            ICacheManager cacheManager,
            ICustomConfigAppService customConfigService,
            IJwtTokenProvider jwtTokenProvider,
            IHttpContextAccessor httpContextAccessor,
            IUserSession userSession) : base(serviceProvider)
        {
            _userDomainManager = userDomainManager;
            _captcha = captcha;
            _idGenerator = idGenerator;
            _cacheManager = cacheManager;
            _customConfigService = customConfigService;
            _jwtTokenProvider = jwtTokenProvider;
            _httpContextAccessor = httpContextAccessor;
            _userSession = userSession;
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <returns></returns>
        public async Task<ZUserInfo> Create()
        {

            
            var dfs = await _userDomainManager.QueryAsNoTracking.FirstOrDefaultAsync();

            //await _userDomainManager.Delete("6e37cc6e9b1948dba987d07b25ffc138");
            //await _userDomainManager.Delete("acab70064e8a45a0bef2074b42d9165e");
            var df1 = ObjectMapper.Map<ZUserInfoDto>(dfs);
            HttpExtension.Fill(new { df1.UserName, df1.PassWord });
            return dfs;
        }

        /// <summary>
        /// 查询一个用户
        /// </summary>
        /// <returns></returns>
        public async Task<List<ZUserInfoDto>> GetFrist()
        {
            var dfs = await _userDomainManager.QueryAsNoTracking.ToListAsync();

            return  ObjectMapper.Map<List<ZUserInfoDto>>(dfs);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
		public async Task<ZUserInfoDto?> Login(ZUserInfoDto user)
		{
			var dfs = await _userDomainManager.QueryAsNoTracking.FirstOrDefaultAsync(P=>P.UserName == user.UserName && P.PassWord == user.PassWord);
			
            if (dfs == null)
                return default;

            return ObjectMapper.Map<ZUserInfoDto>(dfs);
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

            //if (user.PassWord != MD5Encryption.Encrypt($"{_idGenerator.Encode(user.Id)}{dto.PassWord}"))
            //{
            //    await _cacheManager.SetCacheAsync(signInErrorCacheKey, value + 1, TimeSpan.FromMinutes(5));
            //    throw new UserFriendlyException("用户名或密码错误");
            //}
            UserTokenModel tokenModel = new UserTokenModel();
            tokenModel.UserName = user.UserName!;
            tokenModel.UserId = user.Id!;
            var token = _jwtTokenProvider.GenerateAccessToken(tokenModel);
            var context = _httpContextAccessor.HttpContext;
            context.Response.Cookies.Append("access-token", token, new CookieOptions()
            {
                Expires = DateTimeOffset.UtcNow.AddDays(1)
            });

            var claimsIdentity = new ClaimsIdentity(tokenModel.Claims, "Login");

            AuthenticationProperties properties = new AuthenticationProperties();
            properties.AllowRefresh = true;
            properties.IsPersistent = true;
            properties.IssuedUtc = DateTimeOffset.UtcNow;
            properties.ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1);

            await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);
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


        [DisplayName("获取登录用户的信息")]
        [HttpGet]
        public async Task<ZUserInfoOutput> CurrentUserInfo()
        {
            var userId = _userSession.UserId;
            return await _userDomainManager.QueryAsNoTracking.Where(x => x.Id == userId)
                  .Select(x => new ZUserInfoOutput
                  {
                      Name = x.Name,
                      UserName = x.UserName,
                      Avatar = x.Avatar,
                      Birthday = x.Birthday,
                      Email = x.Email,
                      Gender = x.Gender,
                      LastLoginIp = x.LastLoginIp,
                      LastLoginAddress = x.LastLoginAddress,
                      Mobile = x.Mobile,
                      OrgId = x.OrgId,
                      //OrgName = SqlFunc.Subqueryable<SysOrganization>().Where(o => o.Id == x.OrgId).Select(o => o.Name)
                  })
                  //.Mapper(dto =>
                  //{
                  //    if (_authManager.IsSuperAdmin)
                  //    {
                  //        dto.AuthBtnList = _repository.AsSugarClient().Queryable<SysMenu>().Where(x => x.Type == MenuType.Button)
                  //              .Select(x => x.Code).ToList();
                  //    }
                  //    else
                  //    {
                  //        var list = _sysMenuService.GetAuthButtonCodeList(userId).GetAwaiter().GetResult();
                  //        dto.AuthBtnList = list.Where(x => x.Access).Select(x => x.Code).ToList();
                  //    }
                  //})
                  .FirstAsync();
        }

    }


}
