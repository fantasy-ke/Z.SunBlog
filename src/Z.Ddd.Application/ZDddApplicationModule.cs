using Z.Ddd.Domain;
using Z.Module.Modules;

namespace Z.Ddd.Application
{
    [DependOn(typeof(ZDddDomainModule))]
    public class ZDddApplicationModule : ZModule
    {

    }
}