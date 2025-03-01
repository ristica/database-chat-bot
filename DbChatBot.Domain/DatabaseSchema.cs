using DbChatBot.Domain.Contracts;

namespace DbChatBot.Domain;

public class DatabaseSchema : IDatabaseSchema
{
    public List<ITableSchema>? SchemaStructured { get; set; } = [];
    public List<string>? SchemaRaw { get; set; } = [];
}