using Castle.DynamicProxy;

namespace Z.DynamicProxy;

public class ZAsyncDeterminationInterceptor<TInterceptor> : AsyncDeterminationInterceptor
    where TInterceptor : IZInterceptor
{
    public ZAsyncDeterminationInterceptor(TInterceptor zInterceptor)
        : base(new CastleAsyncZInterceptorAdapter<TInterceptor>(zInterceptor))
    {

    }
}
