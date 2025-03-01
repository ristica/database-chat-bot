using DbChatBot.Domain.Contracts;

namespace DbChatBot.Domain;

public interface IDatabaseSchema
{
    List<ITableSchema>? SchemaStructured { get; set; }
    List<string>? SchemaRaw { get; set; }
}