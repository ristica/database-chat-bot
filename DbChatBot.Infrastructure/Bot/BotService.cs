using System.ClientModel;
using System.Text;
using Azure.AI.OpenAI;
using DbChatBot.Constants;
using DbChatBot.Infrastructure.Contracts;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;

namespace DbChatBot.Infrastructure.Bot;

public class BotService(IConfiguration config) : IBotService
{
    public async Task<IChatClient?> CreateChatClient(
        string endpoint,
        string model,
        string serviceName,
        string? aiKey = null)
    {
        return serviceName switch
        {
            AiProvider.AzureOpenAi => 
                await Task.FromResult(
                    new AzureOpenAIClient(
                        new Uri(endpoint), 
                        new ApiKeyCredential(aiKey!))
                        .AsChatClient(model)),
            AiProvider.Ollama => 
                await Task.FromResult(
                    new OllamaChatClient(
                        new Uri(endpoint), 
                        model)),
            _ => null
        };
    }

    public async Task<List<ChatMessage>?> GetChatHistory(
        string serviceName,
        string[]? schemaColumns,
        string? databaseType,
        string? prompt)
    {
        var chatHistory = new List<ChatMessage>();
        var builder = new StringBuilder();
        var maxRows = config["MAX_ROWS"] ?? "10";

        builder.AppendLine(
            "Your are a helpful, cheerful database assistant. " +
            "Do not respond with any information unrelated to databases or queries. " +
            "Use the following database schema when creating your answers:");

        foreach (var table in schemaColumns!) builder.AppendLine(table);

        builder.AppendLine("Include column name headers in the query results.");
        builder.AppendLine("Always provide your answer in the JSON format below:");
        builder.AppendLine(@"{ ""summary"": ""your-summary"", ""query"":  ""your-query"" }");
        builder.AppendLine("Output ONLY JSON formatted on a single line. Do not use new line characters.");
        builder.AppendLine(
            @"In the preceding JSON response, substitute ""your-query"" with the database query used to retrieve the requested data.");
        builder.AppendLine(
            @"In the preceding JSON response, substitute ""your-summary"" with an explanation of each step you took to create this query in a detailed paragraph.");
        builder.AppendLine($"Only use {databaseType} syntax for database queries.");
        builder.AppendLine($"Always limit the SQL query to {maxRows} rows.");
        builder.AppendLine("Always include all of the table columns and details.");

        chatHistory.Add(serviceName.EndsWith(AiProvider.Ollama)
            ? new ChatMessage(ChatRole.System, builder.ToString())
            : new ChatMessage(ChatRole.User, builder.ToString()));

        chatHistory.Add(new ChatMessage(ChatRole.User, prompt));

        return await Task.FromResult(chatHistory);
    }

    public async Task<string?> GenerateQuery(
        IChatClient chatClient,
        List<ChatMessage> chatHistory)
    {
        try
        {
            var response = await chatClient.GetResponseAsync(chatHistory);
            return response.Message.Text?.Replace("```json", "").Replace("```", "").Replace("\\n", " ");
        }
        catch (Exception e)
        {
            return null;
        }
    }
}