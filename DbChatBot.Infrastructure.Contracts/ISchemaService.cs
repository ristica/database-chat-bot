using DbChatBot.Domain;

namespace DbChatBot.Infrastructure.Contracts;

public interface ISchemaService
{
    Task<IDatabaseSchema> GenerateSchema(
        string? connectionString,
        string? databaseType);
}