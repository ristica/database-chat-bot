namespace DbChatBot.Domain.Contracts;

public interface IAiQuery
{
    string? summary { get; set; }
    string? query { get; set; }
}