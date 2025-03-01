using DbChatBot.Infrastructure.Contracts;
using Npgsql;

namespace DbChatBot.Infrastructure.Table.Implementations;

public class PostgresSqlDatabaseService : IDatabaseService
{
    public async Task<List<List<string>>?> GetDataTable(
        string connectionString,
        string databaseType,
        string sqlQuery)
    {
        var dataSource = NpgsqlDataSource.Create(connectionString);

        await using var command = dataSource.CreateCommand(sqlQuery);
        await using var reader = await command.ExecuteReaderAsync();

        return await DatabaseServiceBusinessEngine.GenerateRows(reader);
    }
}