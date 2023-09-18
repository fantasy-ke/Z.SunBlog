# Z.NetWiki

## 个人根据abp vnext搭建的基础小框架用来学习

- abp vnext 模块依赖（已完成）

- 自动注册（已完成）

  - ITransientDependency（瞬时）
  - ISingletonDependency（单例）
  - IScopedDependency（作用域）

- EF基础仓储实现

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
