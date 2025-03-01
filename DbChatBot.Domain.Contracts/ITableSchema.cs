namespace DbChatBot.Domain.Contracts;

public interface ITableSchema
{
    string? TableName { get; set; }
    List<string?> Columns { get; set; }
}