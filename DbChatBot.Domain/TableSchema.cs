using DbChatBot.Domain.Contracts;

namespace DbChatBot.Domain;

public class TableSchema : ITableSchema
{
    public string? TableName { get; set; }
    public List<string?> Columns { get; set; } = [];
}