using Serilog;
using Z.Ddd.Common.Serilog;
using Z.Ddd.Common.Serilog.Utility;
using Z.Module.Extensions;
using Z.NetWiki.Host;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddHttpContextAccessor();



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

app.UseSerilogRequestLogging(options =>
{
    options.MessageTemplate = SerilogRequestUtility.HttpMessageTemplate;
    options.GetLevel = SerilogRequestUtility.GetRequestLevel;
    options.EnrichDiagnosticContext = SerilogRequestUtility.EnrichFromRequest;
});

app.Run();
