using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Z.Module.DependencyInjection;

namespace Z.Ddd.Domain.UserSession
{
    public class CurrentUserSession : IUserSession, ITransientDependency
    {

        private readonly IHttpContextAccessor _httpContextAccessor;


        public CurrentUserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserId { get; protected set; }

        public string UserName { get; protected set; }

        public IEnumerable<string>? RoleName { get; protected set; }

        public IEnumerable<string>? RoleIds { get; protected set; }

        public virtual void SetUserInfo()
        {
            var identity = _httpContextAccessor?.HttpContext?.User?.Identity;
            var httpContext = _httpContextAccessor?.HttpContext;
            var isAuthenticated = identity?.IsAuthenticated ?? false;
            var claims = _httpContextAccessor?.HttpContext?.User?.Claims;
            var userId = claims?.FirstOrDefault(p => p.Type == "Id")?.Value;
            if (userId is null || !isAuthenticated)
            {
                //NotLoginExceptionsExtensions.ThrowNotloginExceptions();
            }
            UserId = userId!.ToString();
            UserName = claims?.FirstOrDefault(p => p.Type == ClaimTypes.Name)?.Value ?? "";
            RoleIds = claims?
                      .Where(p => p?.Type?.Equals("RoleIds") ?? false)
                      .Select(p => p.Value);
            RoleName = claims?
                   .Where(p => p?.Type?.Equals(ClaimTypes.Role) ?? false)
                   .Select(p => p.Value);
        }
    }
}
