using Z.DynamicProxy.Extensions;
using Z.HangFire.Builder;
using Z.Module;
using Z.Module.Extensions;
using Z.Module.Modules;

namespace Z.HangFire;

public class ZHangFireModule : ZModule
{
    public override void ConfigureServices(ServiceConfigerContext context)
    {
        context.Services.ConfigureHangfireService();
    }

    public override void OnInitApplication(InitApplicationContext context)
    {
        var app = context.GetApplicationBuilder();
        app.UseZHangfire();
    }
}
