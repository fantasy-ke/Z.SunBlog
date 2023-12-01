using Z.Module.DependencyInjection;

namespace Z.Ddd.Common.Authorization.Authorize
{
    public interface IAuthorizeManager : ITransientDependency
    {
        Task AddAuthorizeRegiester(IAuthorizePermissionContext AuthorizePermissionContext);
    }
}
