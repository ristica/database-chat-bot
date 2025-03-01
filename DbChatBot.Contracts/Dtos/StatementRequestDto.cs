namespace DbChatBot.Contracts.Dtos;

public class StatementRequestDto
{
    public string? OpenAiEndpoint { get; set; }
    public string? OpenAiKey { get; set; }
    public string? ModelName { get; set; }
    public string? ServiceName { get; set; }
    public string? Prompt { get; set; }
    public string[]? Schema { get; set; }
    public string? DatabaseType { get; set; }
}