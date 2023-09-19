# Z.NetWiki

## 个人根据abp vnext搭建的基础小框架用来学习

- abp vnext 模块依赖（已完成）

  - ``````C#
    namespace Z.NetWiki.Host;
    
    [DependOn(typeof(NetWikiApplicationModule),
        typeof(NetWikiEntityFrameworkCoreModule))]
    public class NetWikiHostModule : ZModule
    {
        /// <summary>
        /// 服务配置
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ServiceConfigerContext context)
        {
            configuration = context.GetConfiguration();
            env = context.Environment();
            context.Services.AddControllers();
            context.Services.AddCors(
                options => options.AddPolicy(
                    name: "ZCores",
                    builder => builder.WithOrigins(
                        configuration["App:CorsOrigins"]!
                        .Split(",", StringSplitOptions.RemoveEmptyEntries)//获取移除空白字符串
                        .Select(o => o.RemoveFix("/"))
                        .ToArray()
                        )
                ));
    
        }
    
        /// <summary>
        /// 初始化应用
        /// </summary>
        /// <param name="context"></param>
        public override void OnInitApplication(InitApplicationContext context)
        {
            var app = context.GetApplicationBuilder();
    
           
            app.UseRouting();
    
            app.UseCors("ZCores");
    
            app.UseAuthorization();
    
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
    

- 自动注册（已完成）

  - ITransientDependency（瞬时）

    - ``````C#
      public class JwtTokenProvider : IJwtTokenProvider , ITransientDependency
      {
      
      public interface IJwtTokenProvider
      {
      }

  - ISingletonDependency（单例）

    - ``````C#
      public class JwtTokenProvider : IJwtTokenProvider , ISingletonDependency
      {
      
      public interface IJwtTokenProvider
      {
      }

  - IScopedDependency（作用域）

    - ``````c#
      public class JwtTokenProvider : IJwtTokenProvider , IScopedDependency
      {
      
      public interface IJwtTokenProvider
      {
      }

## EF基础仓储实现

- 实现了对`sqlserver`以及`Mysql`的支持

  - **使用SqlServer**

  - ```C#
    using Autofac.Core;
    using Z.EntityFrameworkCore.SqlServer;
    using Z.EntityFrameworkCore.SqlServer.Extensions;
    using Z.Module;
    using Z.Module.Modules;
    
    namespace Z.NetWiki.EntityFrameworkCore
    {
        [DependOn(typeof(ZSqlServerEntityFrameworkCoreModule))]
        public class NetWikiEntityFrameworkCoreModule : ZModule
        {
            public override void ConfigureServices(ServiceConfigerContext context)
            {
              context.AddSqlServerEfCoreEntityFrameworkCore<NetWikiDbContext>();
            }
        }
    }
    ```

  - **使用Mysql**

  - ``````c#
    using Autofac.Core;
    using Z.EntityFrameworkCore.SqlServer;
    using Z.EntityFrameworkCore.SqlServer.Extensions;
    using Z.Module;
    using Z.Module.Modules;
    
    namespace Z.NetWiki.EntityFrameworkCore
    {
        [DependOn(typeof(ZMysqlEntityFrameworkCoreModule))]
        public class NetWikiEntityFrameworkCoreModule : ZModule
        {
            public override void ConfigureServices(ServiceConfigerContext context)
            {
                context.AddMysqlEfCoreEntityFrameworkCore<NetWikiDbContext>();
            }
        }
    }
    ``````
