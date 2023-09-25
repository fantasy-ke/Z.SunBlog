using Autofac.Core;
using Z.EntityFrameworkCore;
using Z.EntityFrameworkCore.SqlServer;
using Z.EntityFrameworkCore.SqlServer.Extensions;
using Z.Module;
using Z.Module.Modules;
using Z.NetWiki.EntityFrameworkCore.EntityFrameworkCore.Seed;

namespace Z.NetWiki.EntityFrameworkCore
{
    [DependOn(typeof(ZSqlServerEntityFrameworkCoreModule))]
    public class NetWikiEntityFrameworkCoreModule : ZModule
    {
        public override void ConfigureServices(ServiceConfigerContext context)
        {
            context.AddSqlServerEfCoreEntityFrameworkCore<NetWikiDbContext>();
            context.UseRepository<NetWikiDbContext>();
        }

        public override void PostInitApplication(InitApplicationContext context)
        {
            //SeedHelper.SeedHostDb(context.ServiceProvider);
        }
    }
}