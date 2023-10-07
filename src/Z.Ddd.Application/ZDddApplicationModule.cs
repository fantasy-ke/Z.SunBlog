using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Z.Ddd.Common;
using Z.Ddd.Common.AutoMapper;
using Z.Module;
using Z.Module.Modules;

namespace Z.Ddd.Application
{
    [DependOn(typeof(ZDddCommonModule))]
    public class ZDddApplicationModule : ZModule
    {
        public override void ConfigureServices(ServiceConfigerContext context)
        {
            //注册AutoMapper
            context.Services.AddSingleton(MapperHepler.CreateMappings);
            context.Services.AddSingleton<IMapper>(provider => provider.GetRequiredService<Mapper>());
        }

    }
}