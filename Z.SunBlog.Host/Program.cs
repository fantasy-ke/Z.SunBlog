using Cuemon.Diagnostics;
using Serilog;
using Serilog.Events;
using Z.Ddd.Common;
using Z.Ddd.Common.Serilog;
using Z.Module.Extensions;
using Z.SunBlog.Host;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;

//int.TryParse(configuration["App:WebHostPort"], out var port);

//port = port == default ? 80 : port;

//builder.WebHost.UseKestrel(op => op.ListenAnyIP(port));

builder.Services.AddHttpContextAccessor();

builder.Logging.ClearProviders();

AppSettings.ConfigurationBuilder(builder.Services,AppContext.BaseDirectory);

builder.Host.AddSerilogSetup();

builder.Services.AddApplication<SunBlogHostModule>();


var app = builder.Build();

app.InitApplication();
app.Run();