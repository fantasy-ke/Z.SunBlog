using System.Reflection;
using Z.Module.Modules;

namespace Z.EntityFrameworkCore.SqlServer
{
    [DependOn(typeof(ZEnityFrameworkCoreModule))]
    public class ZSqlServerEntityFrameworkCoreModule : ZModule
    {

    }
}