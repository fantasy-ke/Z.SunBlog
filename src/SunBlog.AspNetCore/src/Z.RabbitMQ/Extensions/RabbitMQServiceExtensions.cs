using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Z.RabbitMQ.Manager;
using Z.RabbitMQ.RabbitStore;

namespace Z.RabbitMQ.Extensions;

public static class RabbitMQServiceExtensions
{
    /// <summary>
    /// 添加RabbitMq服务注册
    /// </summary>
    /// <param name="services"></param>
    public static IServiceCollection ServiceRabbitMQ(
        this IServiceCollection services,
        Action<ConnectionFactory> factoryAction = null
    )
    {
        using ServiceProvider provider = services.BuildServiceProvider();
        IConfiguration configuration = provider.GetRequiredService<IConfiguration>();
        if (configuration == null)
        {
            throw new ArgumentNullException(nameof(IConfiguration));
        }
        var rabbitOption = configuration.GetSection("App:RabbitMQ").Get<RabbitMQOptions>()!;
        if (!rabbitOption.Enable)
            return services;
        services.AddSingleton<IRabbitConnectionStore, RabbitConnectionStore>();
        services.AddSingleton<IRabbitPolicyStore, RabbitPolicyStore>();
        services.AddSingleton<IRabbitSettingStore>(sp =>
        {
            var logger = sp.GetRequiredService<ILogger<RabbitSettingStore>>();
            var factory = new ConnectionFactory()
            {
                HostName = rabbitOption.Connection,
                DispatchConsumersAsync = true
            };

            if (!string.IsNullOrEmpty(rabbitOption.UserName))
            {
                factory.UserName = rabbitOption.UserName;
            }

            if (!string.IsNullOrEmpty(rabbitOption.Password))
            {
                factory.Password = rabbitOption.Password;
            }

            if (rabbitOption.Port > 0)
            {
                factory.Port = rabbitOption.Port;
            }

            var retryCount = 5;
            if (rabbitOption.RetryCount > 0)
            {
                retryCount = rabbitOption.RetryCount;
            }
            factory.DispatchConsumersAsync = true;
            factoryAction?.Invoke(factory);
            return new RabbitSettingStore(factory, logger);
        });
        // 注册序列化传输服务
        services.AddSingleton<IMsgPackTransmit, MsgPackTransmit>();
        // 注册发布订阅服务
        services.AddTransient<IRabbitEventManager, RabbitEventManager>();

        return services;
    }
}
