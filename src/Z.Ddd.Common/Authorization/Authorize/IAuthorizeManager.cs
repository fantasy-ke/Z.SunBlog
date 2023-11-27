namespace Z.Ddd.Common.Authorization.Authorize
{
    public interface IAuthorizeManager
    {
        Task AddAuthorizeRegiester(IAuthorizePermissionContext AuthorizePermissionContext);
    }
}
