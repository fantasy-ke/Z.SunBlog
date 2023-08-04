using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Z.Ddd.Domain.Authorization;
using Z.Module;
using Z.Module.Modules;

namespace Z.Ddd.Domain
{
    public class ZDddDomainModule : ZModule
    {
        public override void ConfigureServices(ServiceConfigerContext context)
        {
            //context.UseAutofac();
        }
    }
}