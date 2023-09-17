using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Z.Ddd.Common.Authorization;
using Z.Module;
using Z.Module.Modules;

namespace Z.Ddd.Common
{
    public class ZDddDomainModule : ZModule
    {
        public override void ConfigureServices(ServiceConfigerContext context)
        {
            //context.UseAutofac();
        }
    }
}