namespace Z.SunBlog.Application.OAuthModule.Dto;

public class FriendLinkOutput
{
    /// <summary>
    /// 友链ID
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// 友链
    /// </summary>
    public string Link { get; set; }
    /// <summary>
    /// logo
    /// </summary>
    public string Logo { get; set; }
    /// <summary>
    /// 站点名称
    /// </summary>
    public string SiteName { get; set; }
    /// <summary>
    /// 网站描述
    /// </summary>
    public string Remark { get; set; }
}