using DbChatBot.Infrastructure.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace DbChatBot.Infrastructure.Table;

public class GenericDatabaseService(
    IServiceProvider serviceProvider)
    : IGenericDatabaseService
{
    public async Task<List<List<string>>?> GetDataTable(
        string connectionString,
        string databaseType,
        string sqlQuery)
    {
        return
            await
                serviceProvider
                    .GetRequiredKeyedService<IDatabaseService>(databaseType)
                    .GetDataTable(
                        connectionString,
                        databaseType,
                        sqlQuery);
    }
}