using DbChatBot.Domain;
using DbChatBot.Infrastructure.Contracts;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace DbChatBot.Infrastructure.Schema.Implementations;

public class MsSqlSchemaService(
    IDatabaseSchema databaseSchema,
    IServiceProvider serviceProvider,
    ILogger<MsSqlSchemaService> logger) : ISchemaService
{
    public async Task<IDatabaseSchema> GenerateSchema(
        string? connectionString,
        string? databaseType)
    {
        List<KeyValuePair<string?, string?>> rows = [];

        const string sql = @"SELECT SCHEMA_NAME(schema_id) + '.' + o.Name AS 'TableName', c.Name as 'ColumName'
                FROM     sys.columns c
                         JOIN sys.objects o ON o.object_id = c.object_id
                WHERE    o.type = 'U'
                ORDER BY o.Name";

        await using (var connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();
            await using (var command = new SqlCommand(sql, connection))
            {
                await using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                        rows.Add(
                            new KeyValuePair<string?, string?>(reader.GetValue(0).ToString(),
                                reader.GetValue(1).ToString()));
                }
            }
        }

        return SchemaServiceBusinessEngine.GenerateSchema(
           databaseSchema,
           serviceProvider,
           rows);
    }
}