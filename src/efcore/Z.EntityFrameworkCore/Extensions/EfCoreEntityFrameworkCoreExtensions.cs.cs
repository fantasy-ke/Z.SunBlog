using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.Ddd.Common.UnitOfWork;
using Z.EntityFrameworkCore.Core;
using Z.EntityFrameworkCore.Middlewares;
using Z.EntityFrameworkCore.Options;

namespace Z.EntityFrameworkCore.Extensions;

public static class EfCoreEntityFrameworkCoreExtensions
{
    /// <summary>
    /// 注册efCore基础服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="optionsAction"></param>
    /// <param name="lifetime"></param>
    /// <typeparam name="TDbContext"></typeparam>
    /// <returns></returns>
    public static IServiceCollection AddEfCoreEntityFrameworkCore<TDbContext>(this IServiceCollection services, Action<DbContextOptionsBuilder>? optionsAction = null,
        ServiceLifetime lifetime = ServiceLifetime.Scoped)
        where TDbContext : ZDbContext<TDbContext>
    {
        ConfigureOptions(services);
        ConfigureDbContext<TDbContext>(services, optionsAction, lifetime);

        // 注入工作单元

        return services;
    }

    /// <summary>
    /// 注入SqlServer的DbContext
    /// </summary>
    /// <param name="services"></param>
    /// <param name="optionsAction"></param>
    /// <param name="lifetime"></param>
    /// <typeparam name="TDbContext"></typeparam>
    private static void ConfigureDbContext<TDbContext>(IServiceCollection services, Action<DbContextOptionsBuilder>? optionsAction = null,
        ServiceLifetime lifetime = ServiceLifetime.Scoped)
        where TDbContext : DbContext
    {
        services.AddDbContextFactory<TDbContext>(optionsAction, lifetime);
    }

    /// <summary>
    /// 获取Options并且注入
    /// </summary>
    /// <param name="services"></param>
    private static void ConfigureOptions(IServiceCollection services)
    {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        services.Configure<ZDbContextOptions>(configuration.GetSection(ZDbContextOptions.Name));
    }

    /// <summary>
    /// 使用自动工作单元中间件
    /// </summary>
    /// <param name="app"></param>
    public static void UseUnitOfWorkMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<UnitOfWorkMiddleware>();
    }

    /// <summary>
    /// 注入工作单元中间件
    /// </summary>
    /// <param name="services"></param>
    public static IServiceCollection AddUnitOfWorkMiddleware(this IServiceCollection services)
    {
        services.AddTransient<UnitOfWorkMiddleware>();
        return services;
    }
}
