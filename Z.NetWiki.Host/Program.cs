using Serilog;
using Serilog.Events;
using Z.Ddd.Common.Serilog;
using Z.Module.Extensions;
using Z.NetWiki.Host;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddHttpContextAccessor();

builder.Logging.ClearProviders();

builder.Services.AddApplication<NetWikiHostModule>();

builder.Host.AddSerilogSetup();

//builder.Host.UseAutofac();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseAuthorization();

//app.MapControllers();

app.InitApplication();



app.Run();
