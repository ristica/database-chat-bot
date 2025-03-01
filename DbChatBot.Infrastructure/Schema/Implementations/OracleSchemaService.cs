using DbChatBot.Domain;
using DbChatBot.Infrastructure.Contracts;
using Microsoft.Extensions.Logging;

namespace DbChatBot.Infrastructure.Schema.Implementations;

public class OracleSchemaService(
    IDatabaseSchema databaseSchema,
    IServiceProvider serviceProvider,
    ILogger<OracleSchemaService> logger) : ISchemaService
{
    public async Task<IDatabaseSchema> GenerateSchema(
        string? connectionString, 
        string? databaseType)
    {
        throw new NotImplementedException();
    }
}