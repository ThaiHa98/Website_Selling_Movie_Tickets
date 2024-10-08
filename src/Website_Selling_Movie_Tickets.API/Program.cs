using HRM.API.Extensions;
using HRM.Infrastructure;
using Serilog;
using Website_Selling_Movie_Tickets.API.Extensions;
using Website_Selling_Movie_Tickets.API.Identity.Authorization;
using Website_Selling_Movie_Tickets.Application;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

Log.Information($"Start {builder.Environment.ApplicationName} up");

try
{
    // Add services to the container.
    builder.Host.AddAppConfigurations();
    builder.Services.AddConfigurationSettings(builder.Configuration);
    builder.Services.AddApplicationServices();
    builder.Services.AddInfrastructureServices();
    builder.Services.ConfigureHealthChecks();
    builder.Services.ConfigureServices();
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.ConfigureSwagger();
    builder.Services.AddMemoryCache();
    builder.Services.ConfigureAuthenticationHandler(builder.Configuration);
    builder.Services.ConfigureCors();

    var app = builder.Build();

    app.UseInfrastructure();
    app.UseCors("AllowAngularApp");

    app.Run();
}
catch (Exception ex)
{
    string type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal)) throw;

    Log.Fatal(ex, $"Unhandled exception: {ex.Message}");
}
finally
{
    Log.Information($"Shut down {builder.Environment.ApplicationName} complete");
    Log.CloseAndFlush();
}
