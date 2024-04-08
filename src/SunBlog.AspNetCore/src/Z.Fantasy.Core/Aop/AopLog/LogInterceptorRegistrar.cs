using System;
using System.Linq;
using Z.Foundation.Core.DependencyInjection;
using Z.Foundation.Core.Extensions;
using Z.Foundation.Core.Helper;

namespace Z.Fantasy.Core.Aop.AopLog;

public static class LogInterceptorRegistrar
{
    public static void RegisterIfNeeded(IOnServiceRegistredContext context)
    {
        if (ShouldIntercept(context.ImplementationType))
        {
            context.Interceptors.TryAdd<ZLogAopInterceptor>();
        }
    }

    private static bool ShouldIntercept(Type type)
    {
        if (AppSettings.GetValue("App:LogAOP:Enabled").CastTo(true) && typeof(ILogEnabled).IsAssignableFrom(type))
        {
            return true;
        }

        return false;
    }
}
