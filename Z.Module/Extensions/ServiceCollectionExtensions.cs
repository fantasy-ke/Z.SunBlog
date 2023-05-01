using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Module.Modules.interfaces;
using Z.Module.Modules;
using System.Reflection;
using Z.Module.DependencyInjection;

namespace Z.Module.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static ObjectAccessor<T> AddObjectAccessor<T>(this IServiceCollection services)
        {
            if (services.Any(s => s.ServiceType == typeof(T)))
            {
                throw new Exception("该对象已经注册成功过 " + typeof(T).AssemblyQualifiedName);
            }
            var accessor = new ObjectAccessor<T>();
            //Add to the beginning for fast retrieve
            services.Insert(0, ServiceDescriptor.Singleton(typeof(IObjectAccessor<T>), accessor));
            services.Insert(0, ServiceDescriptor.Singleton(typeof(ObjectAccessor<T>), accessor));
            

            return accessor;
        }

        public static void CheckNull(this IServiceCollection services)
        {
            if (services is null)
            {
                throw new ArgumentException("services is null");
            }
        }

        public static T GetSingletonInstanceOrNull<T>(this IServiceCollection services)
        {
            return (T)services
                .FirstOrDefault(d => d.ServiceType == typeof(T))
                ?.ImplementationInstance;
        }
        public static void ChcekNull(this IServiceCollection services)
        {
            if (services is null)
            {
                throw new ArgumentNullException("IServiceCollection为空");
            }
        }

        public static T GetSingletonInstance<T>(this IServiceCollection services)
        {
            var service = services.GetSingletonInstanceOrNull<T>();
            if (service == null)
            {
                throw new InvalidOperationException("Could not find singleton service: " + typeof(T).AssemblyQualifiedName);
            }

            return service;
        }

        public static IServiceCollection AddAssemblyOf<T>(this IServiceCollection services)
        {
            return services.AddAssembly(typeof(T).GetTypeInfo().Assembly);
        }

        public static IServiceCollection AddAssembly(this IServiceCollection services, Assembly assembly)
        {
            new ConventionalRegistrar().AddAssembly(services, assembly);

            return services;
        }
    }
}
