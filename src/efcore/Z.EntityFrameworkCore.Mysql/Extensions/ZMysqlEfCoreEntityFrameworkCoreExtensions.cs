using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Z.EntityFrameworkCore.Attributes;
using Z.EntityFrameworkCore.Extensions;
using Z.Module;
using Z.Module.Extensions;

namespace Z.EntityFrameworkCore.Mysql.Extensions;

public static class ZMysqlEfCoreEntityFrameworkCoreExtensions
{
    public static ServiceConfigerContext AddMysqlEfCoreEntityFrameworkCore<TDbContext>(this ServiceConfigerContext context,
        Version version, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        where TDbContext : ZDbContext<TDbContext>
    {
        var configuration = context.GetConfiguration();
        var connectString = typeof(TDbContext).GetCustomAttribute<ConnectionStringNameAttribute>();

        var connectionString = connectString?.ConnectionString ?? "Default";

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentNullException(connectionString);
        }

        context.Services.AddEfCoreEntityFrameworkCore<TDbContext>(x =>
        {
            x.UseMySql(configuration.GetConnectionString(connectionString)
                , new MySqlServerVersion(version));

        },serviceLifetime);

        return context;
    }
}