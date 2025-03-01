using Microsoft.Extensions.AI;

namespace DbChatBot.Infrastructure.Contracts;

public interface IBotService
{
    Task<IChatClient?> CreateChatClient(
        string endpoint,
        string model,
        string serviceName,
        string? aiKey = null);

    Task<List<ChatMessage>?> GetChatHistory(
        string serviceName,
        string[]? schemaColumns,
        string? databaseType,
        string? prompt);

    Task<string?> GenerateQuery(
        IChatClient chatClient,
        List<ChatMessage> chatHistory);
}