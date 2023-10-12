using DocumentBuilder.Exceptions;
using MarkdownDocumentBuilder.Model.Elements.Table;
using MarkdownDocumentBuilder.Options;

namespace MarkdownDocumentBuilder.Model.Elements;

internal class Table<TRow> : IMarkdownElement
{
    public TableHeader<TRow> TableHeader { get; set; }
    public TableDivider<TRow> TableDivider { get; set; }
    public TableRows<TRow> TableRows { get; set; }

    public Table(IEnumerable<TRow> tableRows, MarkdownTableOptions options)
    {
        ValidateRows(tableRows);
        TableHeader = new TableHeader<TRow>(tableRows, options);
        TableDivider = new TableDivider<TRow>(tableRows, options);
        TableRows = new TableRows<TRow>(tableRows, options);
    }

    private static void ValidateRows(IEnumerable<TRow> tableRows)
    {
        var genericType = typeof(TRow);
        foreach (var tableRow in tableRows)
        {
            var tableRowType = tableRow?.GetType();
            if (tableRowType != genericType)
            {
                var message = $"The type {tableRowType} does not equal the provided generic parameter {genericType}, base types are not supported";
                throw new MarkdownDocumentBuilderException(MarkdownDocumentBuilderErrorCode.ProvidedGenericTypeForTableDoesNotEqualRunTimeType, message);
            }
        }
    }

    public IEnumerable<MarkdownLine> ToMarkdown()
    {
        var markdownLines = new List<MarkdownLine>();
        var header = TableHeader.ToMarkdown();
        var divider = TableDivider.ToMarkdown();
        var rows = TableRows.ToMarkdown();
        markdownLines.AddRange(header);
        markdownLines.AddRange(divider);
        markdownLines.AddRange(rows);
        return markdownLines;
    }
}