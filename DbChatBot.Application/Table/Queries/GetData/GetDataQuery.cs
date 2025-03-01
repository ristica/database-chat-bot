using DbChatBot.Contracts.Dtos;
using ErrorOr;
using MediatR;

namespace DbChatBot.Application.Table.Queries.GetData;

public sealed record GetDataQuery(
    string? ConnectionString,
    string? DatabaseType,
    string? SqlQuery) : IRequest<ErrorOr<QueryResponseDto>>;