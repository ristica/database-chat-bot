using System.Text.Json;
using AutoMapper;
using DbChatBot.Contracts.Dtos;
using DbChatBot.Contracts.Models;
using DbChatBot.Infrastructure.Contracts;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DbChatBot.Application.Bot.Queries.GetStatement;

public class GetStatementHandler(
    IMapper mapper,
    IBotService botService,
    ILogger<GetStatementHandler> logger)
    : IRequestHandler<GetStatementQuery, ErrorOr<StatementResponseDto>>
{
    public async Task<ErrorOr<StatementResponseDto>> Handle(
        GetStatementQuery request,
        CancellationToken cancellationToken)
    {
        var chatClient =
            await botService.CreateChatClient(
                request.OpenAiEndpoint!,
                request.OpenAiKey!,
                request.ModelName!);

        if (chatClient == null)
            return Error.Failure(
                "Could not create a chat client!");

        var chatHistory =
            await botService.GetChatHistory(
                request.Schema,
                request.DatabaseType,
                request.Prompt);

        if (chatHistory == null)
            return Error.Failure(
                "Could not create chat history");

        var response =
            await botService.GenerateQuery(
                chatClient,
                chatHistory);

        if (string.IsNullOrEmpty(response))
            return Error.Failure(
                "Could not generate a Query");

        var dto = JsonSerializer.Deserialize<GeneratedQueryDto>(response);

        return new StatementResponseDto
        {
            Query = dto?.query,
            Summary = dto?.summary
        };
    }
}