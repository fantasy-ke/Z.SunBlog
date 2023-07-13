using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;
using System.Text;
using Z.Ddd.Domain;
using Z.Ddd.Domain.Extensions;
using Z.Module;
using Z.Module.Extensions;
using Z.Module.Modules;
using Z.NetWiki.Application;
using Z.NetWiki.Common;

namespace Z.NetWiki.Host
{
    [DependOn(typeof(NetWikiApplicationModule))]
    public class NetWikiHostModule : ZModule
    {
        protected IHostEnvironment env { get;private set; }
        protected IConfiguration configuration { get;private set; }

        /// <summary>
        /// 服务配置
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ServiceConfigerContext context)
        {
            configuration = context.GetConfiguration();
            env = context.Environment();
            context.Services.AddControllers();
            context.Services.AddEndpointsApiExplorer();

            context.Services.AddSwaggerGen(options =>
            {
                //过滤器
                options.OperationFilter<AddResponseHeadersFilter>();
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                options.OperationFilter<SecurityRequirementsOperationFilter>();
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "NETWiki API v1",
                    Title = "NETWiki API",
                    Description = "Web API for managing By Z.NETWiki",
                    TermsOfService = new Uri("https://github.com/Fantasy-Ke"),
                    Contact = new OpenApiContact
                    {
                        Name = "github 地址",
                        Url = new Uri("https://github.com/Fantasy-Ke/Z.NetWiki")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "个人博客",
                        Url = new Uri("https://www.cnblogs.com/fantasy-ke/")
                    }
                });
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
                options.OrderActionsBy(o => o.RelativePath);
                //options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                //{
                //    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                //    Name = "Authorization",//jwt默认的参数名称
                //    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                //    Type = SecuritySchemeType.ApiKey
                //});
            });


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

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "NETWiki API V1");
                options.EnableDeepLinking();//深链接功能
                options.DocExpansion(DocExpansion.None);//swagger文档是否打开
                options.IndexStream = () =>
                {
                    var path = Path.Join(env.ContentRootPath, "wwwroot", "pages", "swagger.html");
                    return new FileInfo(path).OpenRead();
                };
            });

            app.UseRouting();


            app.UseCors("ZCores");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }



        private void ServicesJWT(IServiceCollection services)
        {


            var config = configuration.GetSection("App:JWtSetting").Get<JwtSettings>(); // 从appsettings.json读取JwtConfig配置
            // 添加JWT身份验证服务
            services.AddAuthentication(options =>
            {
                options.RequireAuthenticatedSignIn = true;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config.Issuer,
                    ValidAudience = config.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.SecretKey))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = async context =>
                    {
                        var token = context.Request.Cookies["access_token"]; // 从Cookie中获取token值
                        if (!string.IsNullOrEmpty(token))
                        {
                            context.Token = token; // 将token值设置到JwtBearer上下文中的Token属性
                        }
                    }
                };
            });
        }

    }
}
