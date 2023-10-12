namespace Z.NetWiki.Application.TagsModule.BlogServer.Dto;

public class CreateOrUpdateTagInput : AddTagInput
{
    /// <summary>
    /// 文章ID
    /// </summary>
    public Guid? Id { get; set; }
}