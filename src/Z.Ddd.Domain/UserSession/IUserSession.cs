using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z.Ddd.Domain.UserSession
{
    public interface IUserSession
    {
        public string UserId { get; }

        public string UserName { get; }

        public IEnumerable<string>? RoleName { get; }

        public IEnumerable<string>? RoleIds { get; }

        void SetUserInfo();
    }
}
