using System.Data.Common;

namespace DbChatBot.Infrastructure.Table;

public static class DatabaseServiceBusinessEngine
{
    public static async Task<List<List<string>>> GenerateRows(
        DbDataReader reader)
    {
        var rows = new List<List<string>>();

        var headersAdded = false;
        if (!reader.HasRows) return rows;

        while (await reader.ReadAsync())
        {
            var cols = new List<string?>();
            var headerCols = new List<string?>();
            if (!headersAdded)
            {
                for (var i = 0; i < reader.FieldCount; i++) headerCols.Add(reader.GetName(i));
                headersAdded = true;
                rows.Add(headerCols!);
            }

            for (var i = 0; i <= reader.FieldCount - 1; i++)
                try
                {
                    cols.Add(reader.GetValue(i).ToString());
                }
                catch
                {
                    cols.Add("Error");
                }

            rows.Add(cols!);
        }

        return rows;
    }
}