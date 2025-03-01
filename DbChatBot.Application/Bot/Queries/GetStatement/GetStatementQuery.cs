using DbChatBot.Contracts.Dtos;
using ErrorOr;
using MediatR;

namespace DbChatBot.Application.Bot.Queries.GetStatement;

public sealed record GetStatementQuery(
    string? OpenAiEndpoint,
    string? OpenAiKey,
    string? ModelName,
    string? ServiceName,
    string? Prompt,
    string[]? Schema,
    string? DatabaseType)
    : IRequest<ErrorOr<StatementResponseDto>>;