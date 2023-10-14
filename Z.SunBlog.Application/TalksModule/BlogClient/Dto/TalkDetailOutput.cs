namespace Z.SunBlog.Application.TalksModule.BlogClient.Dto;

public class TalkDetailOutput : TalksOutput
{
    /// <summary>
    /// 是否允许评论
    /// </summary>
    public bool IsAllowComments { get; set; }
}