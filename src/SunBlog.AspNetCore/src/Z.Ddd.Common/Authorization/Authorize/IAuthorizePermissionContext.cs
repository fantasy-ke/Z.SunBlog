namespace Z.Ddd.Common.Authorization.Authorize
{
    public interface IAuthorizePermissionContext : IDisposable
    {
        SystemPermission DefinePermission { get; }
    }
}
