using Z.DynamicProxy.Extensions;
using Z.Module;
using Z.Module.Modules;

namespace Z.DynamicProxy;

public class ZCastleCoreModule : ZModule
{
    public override void ConfigureServices(ServiceConfigerContext context)
    {
        context.Services.AddAsyncDeterminationTransient();
        //context.Services.DisableZClassInterceptors();禁用类拦截Aop
    }
}
