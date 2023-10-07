using Microsoft.Extensions.DependencyInjection;
using Z.EntityFrameworkCore;
using Z.EntityFrameworkCore.Core;
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
            var entityManager = context.ServiceProvider
                 .GetRequiredService<IEntityManager<NetWikiDbContext>>();

            //添加种子数据
            entityManager.DbSeed((dbcontext) =>
            {
                SeedHelper.SeedDbData(dbcontext, context.ServiceProvider);
            });
        }
    }
}