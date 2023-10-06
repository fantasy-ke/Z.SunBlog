using Z.Ddd.Application;
using Z.Ddd.Common.AutoMapper;
using Z.Ddd.Common.Entities.Users;
using Z.Module;
using Z.Module.Modules;
using Z.NetWiki.Application.UserModule.Dto;
using Z.NetWiki.Application.UserModule.MapperConfig;
using Z.NetWiki.Common;
using Z.NetWiki.Domain;

namespace Z.NetWiki.Application
{
    [DependOn(typeof(NetWikiDomainModule), typeof(ZDddApplicationModule))]
    public class NetWikiApplicationModule : ZModule
    {
        public override void ConfigureServices(ServiceConfigerContext context)
        {
            ConfigureAutoMapper();
        }

        public override void OnInitApplication(InitApplicationContext context)
        {
           
        }

        private void ConfigureAutoMapper()
        {
            Configure<ZAutoMapperOptions>(options =>
            {
                options.Configurators.Add(ctx =>
                {
                    UserAutoMapper.CreateMappings(ctx.MapperConfiguration);
                });
            });
        }
    }
}