using Microsoft.Extensions.DependencyInjection;
using Z.Ddd.Common.RedisModule;
using Z.Ddd.Common.ResultResponse;
using Z.Module;
using Z.Module.Extensions;
using Z.Module.Modules;

namespace Z.Ddd.Common
{
    public class ZDddCommonModule : ZModule
    {
        public override void ConfigureServices(ServiceConfigerContext context)
        {
            //context.Services.AddAutoMapperSetup();
            var configuration = context.GetConfiguration();

            //redis注册
            context.Services.AddRedis(configuration);

            context.Services.AddControllers(c =>
            {
                c.Filters.Add<ResultFilter>();
            });
            //context.UseAutofac();
        }
    }
}