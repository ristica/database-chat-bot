using DbChatBot.Domain.Contracts;

namespace DbChatBot.Domain;

public class AiConnection : IAiConnection
{
    public string? ConnectionString { get; set; }
    public string? Name { get; set; }
    public string? DatabaseType { get; set; }
}