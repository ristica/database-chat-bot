using DbChatBot.Domain.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace DbChatBot.Domain.Registry;

public static class Bootstrapper
{
    public static IServiceCollection AddDomain(
        this IServiceCollection services)
    {
        services.AddTransient<IAiConnection, AiConnection>();
        services.AddTransient<IAiQuery, AiQuery>();
        services.AddTransient<IDatabaseSchema, DatabaseSchema>();
        services.AddTransient<ITableSchema, TableSchema>();

        return services;
    }
}