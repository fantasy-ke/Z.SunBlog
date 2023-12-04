# Z.SunBlog

#### 该项目已通过阿里云仓库、GithubAction持续集成与部署（[Fantasy-Ke -Z.SunBlog.Blog](http://101.201.118.85:5100/)）


## .NET 8 后端框架
### SunBlog.AspNetCore/src 后端自己搭建的框架

- .Net8
- EF Core 7 适配SqlServer和Mysql
  - 仓储
  - 简单工作单元
- 简单Minio存储桶
- 简单Redis缓存
- 授权管理
- 动态api
- 模块化处理
- AutoMapper (想改成 `Mapster` 对象映射 )
- 简单EventBus (待实现)
- HangFile 后台任务(待实现)
- SignalR实时通信(待实现)
- MongoDb(待实现)
- RabbitMQ(待实现)

![image](https://github.com/Fantasy-Ke/Z.SunBlog/assets/85232349/e6dce492-c4c5-4501-a888-af823b4e406a)


### 模块化类库，参照AbpVnext实现，现已正常使用

- abp vnext 模块依赖（已完成）

  - ``````C#
    namespace Z.SunBlog.Host;
    
    [DependOn(typeof(SunBlogApplicationModule),
        typeof(SunBlogEntityFrameworkCoreModule))]
    public class SunBlogHostModule : ZModule
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
    namespace Z.SunBlog.EntityFrameworkCore
    {
        [DependOn(typeof(ZSqlServerEntityFrameworkCoreModule))]
        public class SunBlogEntityFrameworkCoreModule : ZModule
        {
            public override void ConfigureServices(ServiceConfigerContext context)
            {
              context.AddSqlServerEfCoreEntityFrameworkCore<SunBlogDbContext>();
            }
        }
    }
    ```
    
  - **使用Mysql**
  
  - 依赖模块`ZMysqlEntityFrameworkCoreModule`

  - ``````c#
    namespace Z.SunBlog.EntityFrameworkCore
    {
        [DependOn(typeof(ZMysqlEntityFrameworkCoreModule))]
        public class SunBlogEntityFrameworkCoreModule : ZModule
        {
            public override void ConfigureServices(ServiceConfigerContext context)
            {
                context.AddMysqlEfCoreEntityFrameworkCore<SunBlogDbContext>();
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


## 前台Blog简介

**项目用 Vue3 + TypeScript + Vite4 + Vuex4 + TypeScript + Vuetify + Pinia！**

![image](https://github.com/Fantasy-Ke/Z.SunBlog/assets/85232349/87ba7ffd-2c08-4e6e-a9bb-80813c05f947)


### 环境

-  node 16+  
-  pwsh core  
-  yarn

### 前端主要技术 

所有技术都是当前最新的。

- vue： ^3.3.4
- typescript : ^5.2.2
- vue-router : ^4.2.5
- vite: ^4.4.11
- vuex: ^4.0.0
- axios: ^1.5.1
- highlight.js: ^10.7.2
- marked：^9.1.0


### 前端代理类生成

- cd到目录src/SunBlog.BlogVue的nswag目录
- 调整codeGenerators后端api地址
  ![image](https://github.com/Fantasy-Ke/SunBlog-Vue/assets/85232349/ffe453ac-c45d-43eb-8643-a8f06bca3bc5)
- 双击refresh.bat 执行 



![](https://upload-images.jianshu.io/upload_images/12890819-527034962df50506.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)


## 后台Admin简介

**项目用 Vue3 + TypeScript + Vite4 + Vuex4 + Vue-Router4 + element-plus ！**

![image](https://github.com/Fantasy-Ke/Z.SunBlog/assets/85232349/27f8a9c9-9dbc-4294-bd79-25605afc8ae5)


### 环境

-  node 16+  
-  pwsh core  
-  yarn

### 前端主要技术 

所有技术都是当前最新的。

- vue： ^3.3.4
- typescript : ^5.2.2
- element-plus: ^2.3.14
- vue-router : ^4.2.5
- vite: ^4.4.11
- vuex: ^4.0.0
- axios: ^1.5.1
- highlight.js: ^10.7.2
- marked：^9.1.0


### 前端代理类生成

- cd到目录src/SunBlog.AdminVue 的 nswag目录
- 调整codeGenerators后端api地址
  ![image](https://github.com/Fantasy-Ke/SunBlog-Vue/assets/85232349/ffe453ac-c45d-43eb-8643-a8f06bca3bc5)
- 双击refresh.bat 执行 


## 感谢
- [Easy.Admin](https://gitee.com/miss_you/easy-admin)
  - SunBlog网站前后台基于该开源项目编写，在线网址：https://www.okay123.top/

- [Vue](https://cn.vuejs.org/)
  - 网站前后台前端使用Vue搭建

- [element-plus](https://element-plus.gitee.io/zh-CN/) 
  - Vue组件库

- [vue-next-admin](https://gitee.com/lyt-top/vue-next-admin)
  - 后台前端框架

- [vue-toastification](https://github.com/Maronato/vue-toastification)
  - 一个弹窗组件

还有太多框架未一一列举，感谢开源给予的力量。
