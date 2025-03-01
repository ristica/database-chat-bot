using DbChatBot.Domain;
using DbChatBot.Infrastructure.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace DbChatBot.Infrastructure.Schema;

public class GenericSchemaService(IServiceProvider serviceProvider)
    : IGenericSchemaService
{
    public async Task<IDatabaseSchema> GenerateSchema(
        string? connectionString,
        string? databaseType)
    {
        return
            await
                serviceProvider
                    .GetRequiredKeyedService<ISchemaService>(databaseType)
                    .GenerateSchema(
                        connectionString,
                        databaseType);
    }
}