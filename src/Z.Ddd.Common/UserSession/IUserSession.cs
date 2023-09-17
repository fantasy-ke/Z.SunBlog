using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Z.Ddd.Common.UserSession
{
    public interface IUserSession
    {
        public string UserId { get; }

        public string UserName { get; }

        public IEnumerable<string>? RoleName { get; }

        public IEnumerable<string>? RoleIds { get; }

        void SetUserInfo();

        Claim? FindClaim(string claimType);

        [NotNull]
        Claim[] FindClaims(string claimType);
    }
}
