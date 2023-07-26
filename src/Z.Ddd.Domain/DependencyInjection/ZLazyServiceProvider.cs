using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Module.DependencyInjection;

namespace Z.Ddd.Domain.DependencyInjection
{
    public class ZLazyServiceProvider : IZLazyServiceProvider
    {
        protected IServiceProvider ServiceProvider { get; }
        public ZLazyServiceProvider(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
        public virtual T LazyGetRequiredService<T>()
        {
            return (T)LazyGetRequiredService(typeof(T));
        }

        public virtual object LazyGetRequiredService(Type serviceType)
        {
            return this.GetRequiredService(serviceType);
        }

        public object? GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }
    }


    public interface IZLazyServiceProvider : IServiceProvider
    {
        /// <summary>
        /// This method is equivalent of the GetRequiredService method.
        /// It does exists for backward compatibility.
        /// </summary>
        T LazyGetRequiredService<T>();
    }
}
