using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z.Ddd.Common.Authorization
{
    public class AuthorizeRequirement : IAuthorizationRequirement
    {
        public virtual string[] AuthorizeName { get; private set; }
        public AuthorizeRequirement(params string[] authorizeName)
        {
            AuthorizeName = authorizeName;
        }

        public AuthorizeRequirement() { }
    }
}
