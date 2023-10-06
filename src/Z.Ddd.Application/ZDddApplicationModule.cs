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
            context.Services.AddSingleton(CreateMappings);
            context.Services.AddSingleton<IMapper>(provider => provider.GetRequiredService<Mapper>());
        }


        private Mapper CreateMappings(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var options = scope.ServiceProvider.GetRequiredService<IOptions<ZAutoMapperOptions>>().Value;

                void ConfigureAll(IZAutoMapperConfigurationContext ctx)
                {
                    foreach (var configurator in options.Configurators)
                    {
                        configurator(ctx);
                    }
                }

                options.Configurators.Insert(0, ctx => ctx.MapperConfiguration.ConstructServicesUsing(serviceProvider.GetService));


                //Profile文件
                //void ValidateAll(IConfigurationProvider config)
                //{
                //    foreach (var profileType in options.ValidatingProfiles)
                //    {
                //        config.AssertConfigurationIsValid(((Profile)Activator.CreateInstance(profileType)).ProfileName);
                //    }
                //}

                var mapperConfiguration = new MapperConfiguration(mapperConfigurationExpression =>
                {
                    ConfigureAll(new ZAutoMapperConfigurationContext(mapperConfigurationExpression, scope.ServiceProvider));
                });

                //Profile文件
                //ValidateAll(mapperConfiguration);

                return new Mapper(mapperConfiguration);
            }
        }
    }
}