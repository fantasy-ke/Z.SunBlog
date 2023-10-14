using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Z.Ddd.Application.Middleware;
using Z.Ddd.Common;
using Z.Ddd.Common.AutoMapper;
using Z.Ddd.Common.ResultResponse;
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
            context.Services.AddControllers(c =>
            {
                c.Filters.Add<ModelValidateActionFilterAttribute>();
            });
        }

    }
}