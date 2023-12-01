using Z.Ddd.Common.ResultResponse.Pager;

namespace Z.SunBlog.Application.ConfigModule.Dto;

public class CustomConfigItemQueryInput : Pagination
{
    /// <summary>
    /// 配置ID
    /// </summary>
    public Guid Id { get; set; }
}