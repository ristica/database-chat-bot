using Microsoft.Extensions.AI;

namespace DbChatBot.Infrastructure.Contracts;

public interface IBotService
{
    Task<IChatClient?> CreateChatClient(
        string openAiEndpoint,
        string openAiKey,
        string model);

    Task<List<ChatMessage>?> GetChatHistory(
        string[]? schemaColumns,
        string? databaseType,
        string? prompt);

    Task<string?> GenerateQuery(
        IChatClient chatClient,
        List<ChatMessage> chatHistory);
}