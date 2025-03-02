using DbChatBot.Application.Bot.Queries.GetStatement;
using DbChatBot.Application.Table.Queries.GetData;
using DbChatBot.Contracts.Dtos;
using DbChatBot.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DbChatBot.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DbQueryController(
    ISender sender,
    ILogger<DbQueryController> logger)
    : ApiController
{
    // StatementResponseDto
    [HttpPost]
    [Route("GenerateQuery")]
    public async Task<IActionResult> GenerateQuery(
        StatementRequestDto dto)
    {
        var query = new GetStatementQuery(
            dto.Endpoint,
            dto.AiKey,
            dto.ModelName,
            dto.ServiceName,
            dto.Prompt,
            dto.Schema,
            dto.DatabaseType);

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