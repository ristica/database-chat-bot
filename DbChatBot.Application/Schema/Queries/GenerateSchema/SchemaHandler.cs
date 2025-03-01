using AutoMapper;
using DbChatBot.Contracts.Dtos;
using DbChatBot.Contracts.Models;
using DbChatBot.Infrastructure.Contracts;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DbChatBot.Application.Schema.Queries.GenerateSchema;

public class SchemaHandler(
    IGenericSchemaService genericSchemaService,
    IMapper mapper,
    ILogger<SchemaHandler> logger)
    : IRequestHandler<GenerateSchemaQuery, ErrorOr<DatabaseSchemaResponseDto>>
{
    public async Task<ErrorOr<DatabaseSchemaResponseDto>> Handle(
        GenerateSchemaQuery request,
        CancellationToken cancellationToken)
    {
        var result = 
            await genericSchemaService.GenerateSchema(
                request.ConnectionString!, 
                request.DatabaseType!);

        return new DatabaseSchemaResponseDto
        {
            SchemaRaw = result.SchemaRaw,
            SchemaStructured = mapper.Map<List<TableSchemaDto>>(result.SchemaStructured)
        };
    }
}