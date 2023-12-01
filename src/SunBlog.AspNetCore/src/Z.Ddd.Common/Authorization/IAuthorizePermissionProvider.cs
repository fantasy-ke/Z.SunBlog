using Z.Ddd.Common.Authorization.Authorize;

namespace Z.Ddd.Common.Authorization
{
    public interface IAuthorizePermissionProvider
    {
        void PermissionDefined(IAuthorizePermissionContext context);
    }
}
