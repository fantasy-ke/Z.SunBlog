using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Module.DependencyInjection;

namespace Z.Ddd.Domain.DependencyInjection
{
    public class ZLazyServiceProvider :
        CachedServiceProviderBase,
        IZLazyServiceProvider
    {
        public ZLazyServiceProvider(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }
        public virtual T LazyGetRequiredService<T>()
        {
            return (T)LazyGetRequiredService(typeof(T));
        }

        public virtual object LazyGetRequiredService(Type serviceType)
        {
            return this.GetRequiredService(serviceType);
        }
    }

}
