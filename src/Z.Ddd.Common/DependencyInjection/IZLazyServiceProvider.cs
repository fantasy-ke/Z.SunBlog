using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Module.DependencyInjection;

namespace Z.Ddd.Common.DependencyInjection
{
    public interface IZLazyServiceProvider : ICachedServiceProviderBase
    {
        /// <summary>
        /// This method is equivalent of the GetRequiredService method.
        /// It does exists for backward compatibility.
        /// </summary>
        T LazyGetRequiredService<T>();
    }
}
