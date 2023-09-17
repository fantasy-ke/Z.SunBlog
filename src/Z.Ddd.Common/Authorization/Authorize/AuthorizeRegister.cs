using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Z.Ddd.Common.Authorization.Authorize;

public class AuthorizeRegister
{
    public static List<AuthorizeProvider> AuthorizeProviders { get; private set; }

    public static List<Permission> Permissions { get; private set; }

    public AuthorizeRegister()
    {
        if (AuthorizeProviders is null) throw new ArgumentNullException(nameof(AuthorizeProviders));
    }

    static AuthorizeRegister()
    {
        AuthorizeProviders = AuthorizeProviders ?? new List<AuthorizeProvider>();
        Permissions = Permissions ?? new List<Permission>();
    }


    public static void RegisterAuthorizeProvider<T>() where T : AuthorizeProvider
    {
        var instance = CreateInstance<T>();
        AuthorizeProviders.Add(instance);
    }

    public virtual void RegisterAuthorize<T>() where T : AuthorizeProvider
    {
        var instance = CreateInstance<T>();
        AuthorizeProviders.Add(instance);
    }

    public static T CreateInstance<T>()
    {
        var tye = typeof(T);
        var newExpre = Expression.New(tye);
        var instance = Expression.Lambda<Func<T>>(newExpre).Compile();
        return instance.Invoke();
    }

    public virtual List<Permission> InitAuthorize()
    {
        if (AuthorizeProviders is null) throw new ArgumentNullException(nameof(AuthorizeProviders));
        List<Permission> permissions = new List<Permission>();
        foreach (var provider in AuthorizeProviders)
        {
            GetPermissions(provider.Permissions);
        }
        return permissions;
    }

    private void GetPermissions(Permission permission)
    {
        if (permission is null) throw new ArgumentNullException(nameof(permission));
        Permissions.Add(permission);
        if (permission.Children is null) return;
        foreach (var item in permission.Children)
        {
            GetPermissions(item);
        }
    }
}
