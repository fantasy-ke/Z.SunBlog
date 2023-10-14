using Z.Ddd.Application;
using Z.Ddd.Common.AutoMapper;
using Z.Module;
using Z.Module.Modules;
using Z.NetWiki.Application.AlbumsModule.BlogServer.MapperConfig;
using Z.NetWiki.Application.ArticleModule.BlogServer.MapperConfig;
using Z.NetWiki.Application.TagsModule.BlogServer.MapperConfig;
using Z.NetWiki.Application.TalksModule.BlogServer.MapperConfig;
using Z.NetWiki.Application.UserModule.MapperConfig;
using Z.NetWiki.Core;

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
                    ArticleSAutoMapper.CreateMappings(ctx.MapperConfiguration);
                    AlbumsAutoMapper.CreateMappings(ctx.MapperConfiguration);
                    TagsAutoMapper.CreateMappings(ctx.MapperConfiguration);
                    TalksAutoMapper.CreateMappings(ctx.MapperConfiguration);
                });
            });
        }
    }
}