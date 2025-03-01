namespace DbChatBot.Domain.Contracts;

public interface IAiConnection
{
    string? ConnectionString { get; set; }
    string? Name { get; set; }
    string? DatabaseType { get; set; }
}