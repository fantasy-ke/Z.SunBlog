using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z.Ddd.Common;  
public static class App
{
    /// <summary>
    /// 存储根服务，可能为空
    /// </summary>
    public static IServiceProvider RootServices => AppSettings.RootServices;


    /// <summary>
    /// 获取请求上下文
    /// </summary>
    public static HttpContext HttpContext => CatchOrDefault(() => RootServices.GetRequiredService<IHttpContextAccessor>()?.HttpContext);


    /// <summary>
    /// 处理获取对象异常问题
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="action">获取对象委托</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>T</returns>
    private static T CatchOrDefault<T>(Func<T> action, T defaultValue = null)
        where T : class
    {
        try
        {
            return action();
        }
        catch
        {
            return defaultValue ?? null;
        }
    }
}
