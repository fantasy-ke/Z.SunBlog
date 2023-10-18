using Z.Ddd.Common.ResultResponse;

namespace Z.SunBlog.Application.Dto;

public class CustomConfigItemQueryInput : Pagination
{
    /// <summary>
    /// 配置ID
    /// </summary>
    public Guid Id { get; set; }
}