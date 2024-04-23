using System.Security.Claims;

namespace Z.Foundation.Core.UserSession
{
    public interface IUserSession
    {
        public string UserId { get; }

        public string UserName { get; }

        public IEnumerable<string> RoleName { get; }

        public IEnumerable<string> RoleIds { get; }

        Claim FindClaim(string claimType);

        Claim[] FindClaims(string claimType);
    }
}
