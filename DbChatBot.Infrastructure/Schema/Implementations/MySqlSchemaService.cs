using DbChatBot.Domain;
using DbChatBot.Infrastructure.Contracts;
using Microsoft.Extensions.Logging;
using MySqlConnector;

namespace DbChatBot.Infrastructure.Schema.Implementations;

public class MySqlSchemaService(
    IDatabaseSchema databaseSchema,
    IServiceProvider serviceProvider,
    ILogger<MySqlSchemaService> logger) : ISchemaService
{
    public async Task<IDatabaseSchema> GenerateSchema(
        string? connectionString,
        string? databaseType)
    {
        List<KeyValuePair<string?, string?>> rows = [];

        var pairs = connectionString!.Split(";");
        var database = pairs.FirstOrDefault(x => x.Contains("Database"))?.Split("=").Last();

        var sqlQuery = $@"SELECT 
                                TABLE_NAME, 
                                COLUMN_NAME 
                            FROM 
                                INFORMATION_SCHEMA.COLUMNS 
                            WHERE 
                                TABLE_SCHEMA = '{database}';";

        await using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();
            await using (var command = new MySqlCommand(sqlQuery, connection))
            {
                await using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                        rows.Add(
                            new KeyValuePair<string?, string?>(
                                reader.GetValue(0).ToString(),
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