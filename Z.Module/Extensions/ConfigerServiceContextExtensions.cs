using Microsoft.Extensions.Configuration;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Module.Modules;
using Microsoft.AspNetCore.Builder;
using Z.Module.Modules.interfaces;
using Microsoft.Extensions.Hosting;

namespace Z.Module.Extensions
{
    public static class ConfigerServiceContextExtensions
    {
        /// <summary>
        /// 获取Configuration
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static IConfiguration GetConfiguration(this ServiceConfigerContext context)
        {
            if (context == null && context.Services is null) throw new ArgumentException("ServiceConfigerContext is null");
            return context.Provider.GetRequiredService<IConfiguration>();
        }

        /// <summary>
        /// 获取静态文件
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static IHostEnvironment Environment(this ServiceConfigerContext context)
        {
            if (context is null || context.Services is null) throw new ArgumentException("context is null");
            return context.Provider.GetRequiredService<IHostEnvironment>();
        }

        /// <summary>
        /// 服务注册
        /// </summary>
        /// <typeparam name="TMoudel"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplication<TMoudel>(this IServiceCollection services) where TMoudel : ZModule
        {
            services.ChcekNull();
            services.AddSingleton<IModuleManager, ModuleManager>();
            services.AddObjectAccessor<IApplicationBuilder>();
            //services.TryAddObjectAccessor<IApplicationBuilder>();
            new ZModuleApplicationServiceProvider(typeof(TMoudel), services);
            return services;
        }
    }
}
