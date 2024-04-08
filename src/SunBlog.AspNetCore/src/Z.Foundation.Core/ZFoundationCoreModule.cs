using Z.DynamicProxy;
using Z.Module;
using Z.Module.Modules;

namespace Z.Foundation.Core
{
    [DependOn(typeof(ZCastleCoreModule))]
    public class ZFoundationCoreModule : ZModule
    {
        public override void ConfigureServices(ServiceConfigerContext context)
        {
            
        }

        public override void PostInitApplication(InitApplicationContext context)
        {
            
        }
    }
}