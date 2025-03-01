using DbChatBot.Infrastructure.Contracts;

namespace DbChatBot.Infrastructure.Table.Implementations;

public class OracleDatabaseService : IDatabaseService
{
    public Task<List<List<string>>?> GetDataTable(
        string connectionString,
        string databaseType,
        string sqlQuery)
    {
        throw new NotImplementedException();
    }
}