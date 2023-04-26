using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Module.Modules.interfaces;

namespace Z.Module.Modules
{
    public class PostInitApplicationLifecycleContributor : ModuleLifecycleContributor
    {
        public override void Initialize(InitApplicationContext context,IZModule module)
        {
            (module as IPostInitApplication)?.PostInitApplication(context);
        }
    }

    public class OnInitApplicationModuleLifecycleContributor : ModuleLifecycleContributor
    {
        public override void Initialize(InitApplicationContext context, IZModule module)
        {
            (module as IOnInitApplication)?.OnInitApplication(context);
        }
    }
}
