using Z.Ddd.Common.DomainServiceRegister;

namespace Z.SunBlog.Core.PraiseModule.DomainManager
{
    public class PraiseManager : BusinessDomainService<Praise>, IPraiseManager
    {
        public PraiseManager(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override async Task ValidateOnCreateOrUpdate(Praise entity)
        {
            await Task.CompletedTask;
        }

    }
}
