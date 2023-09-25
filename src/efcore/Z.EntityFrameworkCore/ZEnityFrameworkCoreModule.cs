using Z.Module;
using Z.Module.Modules;

namespace Z.EntityFrameworkCore
{
    public class ZEnityFrameworkCoreModule : ZModule
    {
        public override void ConfigureServices(ServiceConfigerContext context)
        {
           // context.UseRepository<ZDbContext>();
        }
    }
}