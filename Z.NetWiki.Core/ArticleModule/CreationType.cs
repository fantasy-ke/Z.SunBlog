using System.ComponentModel;

namespace Z.NetWiki.Core.ArticleModule;

public enum CreationType
{
    /// <summary>
    /// 原创
    /// </summary>
    [Description("原创")]
    Original,

    /// <summary>
    /// 转载
    /// </summary>
    [Description("转载")]
    Reprinted
}