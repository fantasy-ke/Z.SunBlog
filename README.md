# Z.NetWiki

## 模块化类库，参照AbpVnext实现，现已正常使用

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
            //....
        }
    
        /// <summary>
        /// 初始化应用
        /// </summary>
        /// <param name="context"></param>
        public override void OnInitApplication(InitApplicationContext context)
        {
            var app = context.GetApplicationBuilder();
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

  - 依赖模块`ZSqlServerEntityFrameworkCoreModule`
  
  - ```C#
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
  
  - 依赖模块`ZMysqlEntityFrameworkCoreModule`

  - ``````c#
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

## Serilog配置
- 引入`Z.Ddd.Common.Serilog`命名空间

- 在`Program`中使用

    - ``````c#
        builder.Host.AddSerilogSetup(); //注册Serilog
        ``````

- 在管道中使用`UseSerilogRequestLogging`处理拦截请求信息

    1. `SerilogRequestUtility.HttpMessageTemplate` 模板信息

    2. `SerilogRequestUtility.GetRequestLevel` 请求拦截等级

    3. `SerilogRequestUtility.EnrichFromRequest` 信息获取方法

- ``````c#
    app.UseSerilogRequestLogging(options =>
    {
        options.MessageTemplate = SerilogRequestUtility.HttpMessageTemplate;
        options.GetLevel = SerilogRequestUtility.GetRequestLevel;
        options.EnrichDiagnosticContext = SerilogRequestUtility.EnrichFromRequest;
    });
    ``````