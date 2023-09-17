using Autofac.Core;
using Z.EntityFrameworkCore.SqlServer;
using Z.EntityFrameworkCore.SqlServer.Extensions;
using Z.Module;
using Z.Module.Modules;

namespace Z.NetWiki.EntityFrameworkCore
{
    [DependOn(typeof(ZSqlServerEntityFrameworkCoreModule))]
    public class NetWikiEntityFrameworkCoreModule : ZModule
    {
        public override void ConfigureServices(ServiceConfigerContext context)
        {
            context.AddSqlServerEfCoreEntityFrameworkCore<NetWikiDbContext>();
        }
    }
}