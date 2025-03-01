namespace DbChatBot.Contracts.Dtos;

public class QueryRequestDto
{
    public string? ConnectionString { get; set; }
    public string? DatabaseType { get; set; }
    public string? SqlQuery { get; set; }
}