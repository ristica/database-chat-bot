using DbChatBot.Constants;
using DbChatBot.Infrastructure.Bot;
using DbChatBot.Infrastructure.Contracts;
using DbChatBot.Infrastructure.Schema;
using DbChatBot.Infrastructure.Schema.Implementations;
using DbChatBot.Infrastructure.Table;
using DbChatBot.Infrastructure.Table.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DbChatBot.Infrastructure.Registry;

public static class Bootstrapper
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        services
            .AddServices()
            .AddSettings();

        services.AddTransient<ILoggerFactory, LoggerFactory>();

        return services;
    }

    private static IServiceCollection AddServices(
        this IServiceCollection services)
    {
        services.AddTransient(typeof(IBotService), typeof(BotService));

        services.AddTransient(typeof(IGenericSchemaService), typeof(GenericSchemaService));
        services.AddKeyedTransient<ISchemaService, MsSqlSchemaService>(DatabaseProvider.MsSql);
        services.AddKeyedTransient<ISchemaService, MySqlSchemaService>(DatabaseProvider.MySql);
        services.AddKeyedTransient<ISchemaService, PostgreSqlSchemaService>(DatabaseProvider.PostgresSql);

        services.AddTransient(typeof(IGenericDatabaseService), typeof(GenericDatabaseService));
        services.AddKeyedTransient<IDatabaseService, MsSqlDatabaseService>(DatabaseProvider.MsSql);
        services.AddKeyedTransient<IDatabaseService, MySqlDatabaseService>(DatabaseProvider.MySql);
        services.AddKeyedTransient<IDatabaseService, PostgresSqlDatabaseService>(DatabaseProvider.PostgresSql);

        return services;
    }

    private static IServiceCollection AddSettings(
        this IServiceCollection services)
    {
        // ...

        return services;
    }
}