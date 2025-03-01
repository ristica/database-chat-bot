using System.Text.Json.Serialization;
using DbChatBot.Application;
using DbChatBot.Domain.Registry;
using DbChatBot.Infrastructure.Registry;
using DbChatBot.Registry;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddHttpContextAccessor();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("cors-policy",
            policy => { policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
    });

    builder.Services
        .AddPresentation(builder.Configuration)
        .AddDomain()
        .AddApplication(builder.Configuration)
        .AddInfrastructure()
        .AddEndpointsApiExplorer();

    builder.Services
        .AddControllers()
        .AddJsonOptions(
            options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
            });
    builder.Services.AddAuthentication();
    builder.Services.AddAuthorization();

    builder.Logging.AddLog4Net(GetLog4NetConfigFileName(builder.Configuration));
}

using var app = builder.Build();
{
    app.UseHttpsRedirection();

    app.UseCors("cors-policy");
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}

static string? GetLog4NetConfigFileName(IConfiguration config)
{
    var log4NetFileName = config.GetValue<string>("Log4Net");

    if (string.IsNullOrEmpty(log4NetFileName))
        return null;

    //var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

    //if (env != null)
    //return $"{log4NetFileName}.Development.config";

    //var dotNetVar = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

    //if (dotNetVar != null)
    //    return $"{log4NetFileName}.{dotNetVar}.config";

    return $"{log4NetFileName}.config";
}