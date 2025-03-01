using DbChatBot.Contracts.Models;

namespace DbChatBot.Contracts.Dtos;

public class DatabaseSchemaResponseDto
{
    public List<TableSchemaDto>? SchemaStructured { get; set; } = [];
    public List<string>? SchemaRaw { get; set; } = [];
}