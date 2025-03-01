using DbChatBot.Application.Schema.Queries.GenerateSchema;
using DbChatBot.Application.Table.Queries.GetData;
using DbChatBot.Contracts.Dtos;
using DbChatBot.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DbChatBot.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DbController(
    ISender sender,
    ILogger<DbController> logger)
    : ApiController
{
    // DatabaseSchemaResponseDto
    [HttpPost]
    public async Task<IActionResult> GenerateSchema(DatabaseSchemaRequestDto dto)
    {
        var query = new GenerateSchemaQuery(dto.ConnectionString, dto.DatabaseType);

        var result = await sender.Send(query);

        return result.Match(
            Ok,
            Problem);
    }

    // QueryResponseDto
    [HttpPost]
    [Route("ExecuteQuery")]
    public async Task<IActionResult> ExecuteQuery(
        QueryRequestDto dto)
    {
        var query = new GetDataQuery(
            dto.ConnectionString,
            dto.DatabaseType,
            dto.SqlQuery);

        var result = await sender.Send(query);

        return result.Match(
            Ok,
            Problem);
    }
}