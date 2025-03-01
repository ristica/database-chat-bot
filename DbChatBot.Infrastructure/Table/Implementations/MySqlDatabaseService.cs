using DbChatBot.Infrastructure.Contracts;
using MySqlConnector;

namespace DbChatBot.Infrastructure.Table.Implementations;

public class MySqlDatabaseService : IDatabaseService
{
    public async Task<List<List<string>>?> GetDataTable(
        string connectionString,
        string databaseType,
        string sqlQuery)
    {
        await using var connection = new MySqlConnection(connectionString);

        await connection.OpenAsync();

        await using var command = new MySqlCommand(sqlQuery, connection);
        await using var reader = await command.ExecuteReaderAsync();

        return await DatabaseServiceBusinessEngine.GenerateRows(reader);
    }
}