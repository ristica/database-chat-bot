namespace DbChatBot.Infrastructure.Contracts;

public interface IDatabaseService
{
    Task<List<List<string>>?> GetDataTable(
        string connectionString,
        string databaseType, // used only in generic implementation
        string sqlQuery);
}