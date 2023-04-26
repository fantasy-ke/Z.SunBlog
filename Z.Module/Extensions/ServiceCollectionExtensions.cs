using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Module.Modules.interfaces;
using Z.Module.Modules;

namespace Z.Module.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static ObjectAccessor<T> AddObjectAccessor<T>(this IServiceCollection services, ObjectAccessor<T> accessor)
        {
            if (services.Any(s => s.ServiceType == typeof(ObjectAccessor<T>)))
            {
                throw new Exception("该对象已经注册成功过 " + typeof(T).AssemblyQualifiedName);
            }

            //Add to the beginning for fast retrieve
            services.Insert(0, ServiceDescriptor.Singleton(typeof(ObjectAccessor<T>), accessor));
            services.Insert(0, ServiceDescriptor.Singleton(typeof(IObjectAccessor<T>), accessor));

            return accessor;
        }

        public static void CheckNull(this IServiceCollection services)
        {
            if (services is null)
            {
                throw new ArgumentException("services is null");
            }
        }
    }
}
