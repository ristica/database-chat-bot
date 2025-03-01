using DbChatBot.Infrastructure.Contracts;
using Microsoft.Data.SqlClient;

namespace DbChatBot.Infrastructure.Table.Implementations;

public class MsSqlDatabaseService : IDatabaseService
{
    public async Task<List<List<string>>?> GetDataTable(
        string connectionString,
        string databaseType,
        string sqlQuery)
    {
        await using var connection = new SqlConnection(connectionString);
        await using var command = new SqlCommand(sqlQuery, connection);

        await connection.OpenAsync();
        await using var reader = await command.ExecuteReaderAsync();

        return await DatabaseServiceBusinessEngine.GenerateRows(reader);
    }
}