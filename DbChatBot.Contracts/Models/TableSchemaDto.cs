namespace DbChatBot.Contracts.Models;

public class TableSchemaDto
{
    public string? TableName { get; set; }
    public List<string?> Columns { get; set; } = [];
}