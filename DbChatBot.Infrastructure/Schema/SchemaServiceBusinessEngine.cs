using DbChatBot.Domain;
using DbChatBot.Domain.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace DbChatBot.Infrastructure.Schema;

public static class SchemaServiceBusinessEngine
{
    public static IDatabaseSchema GenerateSchema(
        IDatabaseSchema databaseSchema,
        IServiceProvider serviceProvider,
        List<KeyValuePair<string?, string?>> rows)
    {
        var groups = rows.GroupBy(x => x.Key);

        foreach (var group in groups)
        {
            var tableSchema = serviceProvider.GetRequiredService<ITableSchema>();
            tableSchema.TableName = group.Key;
            tableSchema.Columns = group.Select(x => x.Value).ToList();

            databaseSchema.SchemaStructured?.Add(tableSchema);
        }

        var textLines = (
                from table in databaseSchema.SchemaStructured!
                let schemaLine = $"- {table.TableName} " + $"("
                select table.Columns.Aggregate(schemaLine, (current, column) => current + column + ", ")
                into schemaLine
                select schemaLine + ")"
                into schemaLine
                select schemaLine.Replace(", )", " )"))
            .ToList();

        databaseSchema.SchemaRaw = textLines;

        return databaseSchema;
    }
}