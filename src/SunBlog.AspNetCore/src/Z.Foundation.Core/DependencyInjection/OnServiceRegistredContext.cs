using Z.DynamicProxy;
using Z.Foundation.Core.Exceptions;
using Z.Module.Collections;

namespace Z.Foundation.Core.DependencyInjection;

public class OnServiceRegistredContext : IOnServiceRegistredContext
{
    public virtual ITypeList<IZInterceptor> Interceptors { get; }

    public virtual Type ServiceType { get; }

    public virtual Type ImplementationType { get; }

    public OnServiceRegistredContext(Type serviceType, Type implementationType)
    {
        ServiceType = nameof(serviceType) != null ? serviceType : throw new UserFriendlyException("serviceType¿Õ");
        ImplementationType = nameof(implementationType) != null ? implementationType : throw new UserFriendlyException("implementationType¿Õ");
        Interceptors = new TypeList<IZInterceptor>();
    }
}
