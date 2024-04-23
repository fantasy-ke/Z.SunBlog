namespace Z.Foundation.Core.UserSession
{
    public static class CurrentUserExtensions
    {
        public static string FindClaimValue(this IUserSession currentUser, string claimType)
        {
            return currentUser.FindClaim(claimType)?.Value;
        }
    }
}
