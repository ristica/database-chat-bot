using DbChatBot.Contracts.Dtos;
using ErrorOr;
using MediatR;

namespace DbChatBot.Application.Schema.Queries.GenerateSchema;

public sealed record GenerateSchemaQuery(
    string? ConnectionString,
    string? DatabaseType)
    : IRequest<ErrorOr<DatabaseSchemaResponseDto>>;