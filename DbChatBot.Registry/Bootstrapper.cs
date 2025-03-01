using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace DbChatBot.Registry;

public static class Bootstrapper
{
    public static IServiceCollection AddPresentation(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSwaggerGen(option =>
        {
            option.AddSecurityDefinition(
                "oauth2",
                new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

            option.OperationFilter<SecurityRequirementsOperationFilter>();
        });

        return services;
    }
}