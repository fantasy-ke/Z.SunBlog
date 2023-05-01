using Z.Module;
using Z.Module.Extensions;
using Z.Module.Modules;

namespace Z.NetWiki.Host
{
    public class NetWikiHostModule : ZModule
    {
        public override void ConfigureServices(ServiceConfigerContext context)
        {
            context.Services.AddControllers();
            context.Services.AddEndpointsApiExplorer();
            context.Services.AddSwaggerGen();
        }

        public override void OnInitApplication(InitApplicationContext context)
        {
            var app = context.GetApplicationBuilder();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
