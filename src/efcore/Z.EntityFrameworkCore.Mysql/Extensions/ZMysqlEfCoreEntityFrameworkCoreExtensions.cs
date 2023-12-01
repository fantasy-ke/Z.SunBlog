using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Z.EntityFrameworkCore.Attributes;
using Z.EntityFrameworkCore.Extensions;
using Z.EntityFrameworkCore.Options;
using Z.Module;
using Z.Module.Extensions;

namespace Z.EntityFrameworkCore.Mysql.Extensions;

public static class ZMysqlEfCoreEntityFrameworkCoreExtensions
{
    public static IServiceCollection AddMysqlEfCoreEntityFrameworkCore<TDbContext>(this ServiceConfigerContext context,
        Version version,string connectionString, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        where TDbContext : ZDbContext<TDbContext>
    {

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentNullException(connectionString);
        }

        context.Services.AddEfCoreEntityFrameworkCore<TDbContext>(x =>
        {
            x.UseMySql(connectionString
                , new MySqlServerVersion(version))
            .UseFilter();

        },serviceLifetime);

        return context.Services;
    }


    public static ZDbContextBuilder UseMySql(
        this ZDbContextBuilder builder,
        string connectionString,
         ServerVersion serverVersion,
        Action<MySqlDbContextOptionsBuilder>? mysqlOptionsAction = null)
        => builder.UseMySqlCore(connectionString, serverVersion, mysqlOptionsAction);



    private static ZDbContextBuilder UseMySqlCore(
        this ZDbContextBuilder builder,
        string connectionString,
        ServerVersion serverVersion,
        Action<MySqlDbContextOptionsBuilder>? mysqlOptionsAction)
    {
        builder.Builder = (_, dbContextOptionsBuilder)
            => dbContextOptionsBuilder.UseMySql(connectionString, serverVersion, mysqlOptionsAction);
        return builder;
    }
}