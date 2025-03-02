using DbChatBot.Application.Schema.Queries.GenerateSchema;
using DbChatBot.Contracts.Dtos;
using DbChatBot.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DbChatBot.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DbSchemaController(
    ISender sender,
    ILogger<DbSchemaController> logger)
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

    
}