using Z.Fantasy.Core.DomainServiceRegister.Domain;

namespace Z.SunBlog.Core.PicturesModule.DomainManager;

public class PicturesManager(IServiceProvider serviceProvider)
    : BusinessDomainService<Pictures>(serviceProvider), IPicturesManager
{
    public override async Task ValidateOnCreateOrUpdate(Pictures entity)
    {
        await Task.CompletedTask;
    }

}