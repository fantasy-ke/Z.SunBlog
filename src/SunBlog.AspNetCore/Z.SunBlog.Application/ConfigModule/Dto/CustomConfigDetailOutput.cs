using Newtonsoft.Json.Linq;

namespace Z.SunBlog.Application.ConfigModule.Dto;

public class CustomConfigDetailOutput
{
    /// <summary>
    /// 表单渲染Json
    /// </summary>
    public string FormJson { get; set; }

    /// <summary>
    /// 表单数据
    /// </summary>
    public string DataJson { get; set; }

    /// <summary>
    /// 配置项Id
    /// </summary>
    public Guid? ItemId { get; set; }
}