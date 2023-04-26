using Microsoft.Extensions.Configuration;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Module.Modules;

namespace Z.Module.Extensions
{
    public static class ConfigerServiceContextExtensions
    {
        public static IConfiguration GetConfiguration(this ServiceConfigerContext context)
        {
            if (context == null && context.Services is null) throw new ArgumentException("ServiceConfigerContext is null");
            return context.Provider.GetRequiredService<IConfiguration>();
        }


        public static IServiceCollection AddApplication<TMoudel>(this IServiceCollection services) where TMoudel : ZModule
        {
            services.CheckNull();
        }
    }
}
