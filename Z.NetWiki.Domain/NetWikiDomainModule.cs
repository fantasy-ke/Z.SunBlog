using Z.Module;
using Z.Module.Modules;
using Z.NetWiki.Common;

namespace Z.NetWiki.Domain
{
    [DependOn(typeof(NetWikiCommonModule))]
    public class NetWikiDomainModule : ZModule
    {
        public override void ConfigureServices(ServiceConfigerContext context)
        {
        }

        public override void OnInitApplication(InitApplicationContext context)
        {
        }
    }
}