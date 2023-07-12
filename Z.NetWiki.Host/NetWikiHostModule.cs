using Z.Ddd.Domain.Extensions;
using Z.Module;
using Z.Module.Extensions;
using Z.Module.Modules;
using Z.NetWiki.Application;
using Z.NetWiki.Common;

namespace Z.NetWiki.Host
{
    [DependOn(typeof(NetWikiApplicationModule))]
    public class NetWikiHostModule : ZModule
    {
        public override void ConfigureServices(ServiceConfigerContext context)
        {
            var configuration = context.GetConfiguration();

            context.Services.AddControllers();
            context.Services.AddEndpointsApiExplorer();
            context.Services.AddSwaggerGen();

            context.Services.AddCors(
                options => options.AddPolicy(
                    name: "ZCores",
                    builder => builder.WithOrigins(
                        configuration["App:CorsOriginscors"]!
                        .Split(",", StringSplitOptions.RemoveEmptyEntries)//获取移除空白字符串
                        .Select(o => o.RemoveFix("/"))
                        .ToArray()
                        )
                ));

        }

        public override void OnInitApplication(InitApplicationContext context)
        {
            var app = context.GetApplicationBuilder();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseRouting();


            app.UseCors("ZCores");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
