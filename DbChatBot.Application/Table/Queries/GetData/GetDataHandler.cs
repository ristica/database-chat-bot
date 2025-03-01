using DbChatBot.Contracts.Dtos;
using DbChatBot.Infrastructure.Contracts;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DbChatBot.Application.Table.Queries.GetData;

public class GetDataHandler(
    IGenericDatabaseService genericDatabaseService,
    ILogger<GetDataHandler> logger)
    : IRequestHandler<GetDataQuery, ErrorOr<QueryResponseDto>>
{
    public async Task<ErrorOr<QueryResponseDto>> Handle(
        GetDataQuery request,
        CancellationToken cancellationToken)
    {
        return new QueryResponseDto
        {
            RawData =
                await genericDatabaseService.GetDataTable(
                    request.ConnectionString!,
                    request.DatabaseType!,
                    request.SqlQuery!)
        };
    }
}