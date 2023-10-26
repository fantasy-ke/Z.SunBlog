using Z.Ddd.Common.ResultResponse;

namespace Z.SunBlog.Application.OAuthModule.Dto;

public class AuthAccountPageQueryInput : Pagination
{
    /// <summary>
    /// 昵称
    /// </summary>
    public string? Name { get; set; }
}