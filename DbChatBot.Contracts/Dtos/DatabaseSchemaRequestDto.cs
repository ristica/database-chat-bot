namespace DbChatBot.Contracts.Dtos;

public class DatabaseSchemaRequestDto
{
    public string? ConnectionString { get; set; }
    public string? DatabaseType { get; set; }
}