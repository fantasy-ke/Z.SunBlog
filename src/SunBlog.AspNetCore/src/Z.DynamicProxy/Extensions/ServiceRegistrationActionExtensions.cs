using Microsoft.Extensions.DependencyInjection;

namespace Z.DynamicProxy.Extensions;

public static class ServiceRegistrationActionExtensions
{

    public static void AddAsyncDeterminationTransient(this IServiceCollection services)
    {
        services.AddTransient(typeof(ZAsyncDeterminationInterceptor<>));
    }
}
