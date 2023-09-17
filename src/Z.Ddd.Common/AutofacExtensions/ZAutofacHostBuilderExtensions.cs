using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Z.Module.Extensions;

namespace Z.Ddd.Common.AutofacExtensions;

public static class ZAutofacHostBuilderExtensions
{
    public static IHostBuilder UseAutofac(this IHostBuilder hostBuilder)
    {
        var containerBuilder = new ContainerBuilder();

        return hostBuilder.ConfigureServices((_, services) =>
            {
                services.AddObjectAccessor(containerBuilder);
            })
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>(builder =>
            {
              //  builder.RegisterModule<AutofacPropertityModuleReg>();
            });
    }
}
