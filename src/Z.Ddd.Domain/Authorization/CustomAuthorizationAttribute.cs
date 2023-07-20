using Microsoft.AspNetCore.Authorization;

namespace Z.Ddd.Domain.Authorization;

[AttributeUsage(AttributeTargets.Method)]
public class CustomAuthorizationAttribute: AuthorizeAttribute
{
    public virtual string[] AuthorizeName { get; private set; }

    public CustomAuthorizationAttribute(params string[] authorizeName)
    {
        AuthorizeName = authorizeName;
        Policy = string.Join(",", AuthorizeName);
    }
}
