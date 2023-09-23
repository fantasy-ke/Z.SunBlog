using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Z.Ddd.Common.Authorization;
using Z.Ddd.Common.AutoMapper;
using Z.Module;
using Z.Module.Modules;

namespace Z.Ddd.Common
{
    public class ZDddCommonModule : ZModule
    {
        public override void ConfigureServices(ServiceConfigerContext context)
        {
            context.Services.AddAutoMapperSetup();
            //context.UseAutofac();
        }
    }
}