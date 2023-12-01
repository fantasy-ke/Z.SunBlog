using Z.Module;
using Z.Module.Modules;
using Z.SunBlog.Common;

namespace Z.SunBlog.Core
{
    [DependOn(typeof(SunBlogCommonModule))]
    public class SunBlogCoreModule : ZModule
    {
        public override void ConfigureServices(ServiceConfigerContext context)
        {
        }

        public override void OnInitApplication(InitApplicationContext context)
        {
        }
    }
}