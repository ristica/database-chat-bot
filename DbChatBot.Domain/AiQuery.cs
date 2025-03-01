using DbChatBot.Domain.Contracts;

namespace DbChatBot.Domain;

public class AiQuery : IAiQuery
{
    public string? summary { get; set; }
    public string? query { get; set; }
}