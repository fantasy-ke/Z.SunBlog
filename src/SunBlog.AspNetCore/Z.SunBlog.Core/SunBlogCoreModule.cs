using Microsoft.Extensions.DependencyInjection;
using Z.EventBus.Extensions;
using Z.Module;
using Z.Module.Extensions;
using Z.Module.Modules;
using Z.SunBlog.Common;
using Z.Fantasy.Core.Minio;
using Z.HangFire.Builder;
using Z.OSSCore;
using Z.FreeRedis;

namespace Z.SunBlog.Core
{
    [DependOn(typeof(SunBlogCommonModule))]
    public class SunBlogCoreModule : ZModule
    {
        public override void ConfigureServices(ServiceConfigerContext context)
        {
            var configuration = context.GetConfiguration();
            context.Services.RegisterJobs();
            //redis注册
            context.Services.AddZRedis(configuration, option =>
            {
                option.Capacity = 6;
            });

            // context.Services.AddZMinio(configuration);
            context.Services.AddOSSService("z.host",option =>
            {
                option.Provider = OSSProvider.Minio;
                option.Endpoint = "47.96.234.210:9000";  //不需要带有协议
                option.AccessKey = "CqGL9hDnd1AWq2ZCO2HW";
                option.SecretKey = "PqAKhiQhu5gsoYWcmHKlPBBQaJ8QdNRB1D1lN9hM";
                option.IsEnableHttps = true;
                option.IsEnableCache = true;
            });
            
            context.Services.Configure<MinioConfig>(p =>
            {
                p.DefaultBucket = "z.host";
                p.Host = "47.96.234.210:9000";
            });
            // 注入事件总线
            context.Services.AddEventBus();

            context.Services.AddSignalR()
                .AddMessagePackProtocol();
            //.AddStackExchangeRedis(o =>
            //{
            //    o.ConnectionFactory = async writer =>
            //    {
            //        //使用CsRedis
            //        var cacheOption = configuration.GetSection("App:Cache").Get<CacheOptions>()!;
            //        var connection = await ConnectionMultiplexer.ConnectAsync(cacheOption.RedisEndpoint, writer);
            //        connection.ConnectionFailed += (_, e) =>
            //        {
            //            Console.WriteLine("Connection to Redis failed.");
            //        };

            //        if (!connection.IsConnected)
            //        {
            //            Console.WriteLine("Did not connect to Redis.");
            //        }

            //        return connection;
            //    };
            //});
            
        }

        public override async Task PostInitApplicationAsync(InitApplicationContext context)
        {
            await context.ServiceProvider.InitChannlesAsync();
        }
    }
}