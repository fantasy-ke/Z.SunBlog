using Z.Module;
using Z.Module.Modules;
using Z.NetWiki.Common;
using Z.NetWiki.Domain;

namespace Z.NetWiki.Application
{
    [DependOn(typeof(NetWikiDomainModule))]
    public class NetWikiApplicationModule : ZModule
    {
        public override void ConfigureServices(ServiceConfigerContext context)
        {
        }

        public override void OnInitApplication(InitApplicationContext context)
        {
           
        }
    }
}