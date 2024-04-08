using Z.DynamicProxy;
using Z.Module.Collections;

namespace Z.Foundation.Core.DependencyInjection;

public interface IOnServiceRegistredContext
{
    ITypeList<IZInterceptor> Interceptors { get; }

    Type ImplementationType { get; }
}
