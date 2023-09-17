using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Z.EntityFrameworkCore.Attributes;
using Z.EntityFrameworkCore.Extensions;
using Z.Module;
using Z.Module.Extensions;

namespace Z.EntityFrameworkCore.SqlServer.Extensions;

public static class ZSqlServerEntityFrameworkCoreExtensions
{
    public static IServiceCollection AddSqlServerEfCoreEntityFrameworkCore<TDbContext>(this ServiceConfigerContext context)
        where TDbContext : ZDbContext<TDbContext>
    {
        var configuration = context.GetConfiguration();
        var connectString = typeof(TDbContext).GetCustomAttribute<ConnectionStringNameAttribute>();

        var connectionString = connectString?.ConnectionString ?? "Default";

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentNullException(connectionString);
        }

        context.Services.AddEfCoreEntityFrameworkCore<TDbContext>(
            x =>
            {
                x.UseSqlServer(configuration.GetConnectionString(connectionString));
            },
            ServiceLifetime.Scoped);

        return context.Services;
    }
}