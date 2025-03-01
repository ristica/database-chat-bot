using DbChatBot.Domain;
using DbChatBot.Domain.Contracts;
using DbChatBot.Infrastructure.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace DbChatBot.Infrastructure.Schema.Implementations;

public class PostgreSqlSchemaService(
    IDatabaseSchema databaseSchema,
    IServiceProvider serviceProvider,
    ILogger<PostgreSqlSchemaService> logger) : ISchemaService
{
    public async Task<IDatabaseSchema> GenerateSchema(
        string? connectionString,
        string? databaseType)
    {
        List<KeyValuePair<string?, string?>> rows = [];

        var pairs = connectionString!.Split(";");
        var database = pairs.FirstOrDefault(x => x.Contains("Database"))?.Split("=").Last();

        var sqlQuery = $@"SELECT 
                                table_name, 
                                column_name 
                            FROM 
                                information_schema.columns 
                            WHERE 
                                table_catalog = '{database}'
                                AND table_schema = 'public'
                            ORDER BY 
                                table_name, 
                                column_name;";

        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
        var dataSource = dataSourceBuilder.Build();

        var connection = await dataSource.OpenConnectionAsync();

        await using var cmd = new NpgsqlCommand(sqlQuery, connection);
        await using (var reader = await cmd.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync())
                rows.Add(
                    new KeyValuePair<string?, string?>(
                        reader.GetString(0), 
                        reader.GetString(1)));
        }

        return SchemaServiceBusinessEngine.GenerateSchema(
            databaseSchema, 
            serviceProvider, 
            rows);
    }
}