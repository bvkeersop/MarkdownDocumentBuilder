using MarkdownDocumentBuilder.Extensions;
using MarkdownDocumentBuilder.Model.Document;
using MarkdownDocumentBuilder.Model.Elements.Table.Options;
using System.Text;

namespace MarkdownDocumentBuilder.Model.Elements.Table;

internal class TableHeader<TRow> : TableElement<TRow>, IMarkdownElement
{
    public TableHeader(IEnumerable<TRow> tableRows, MarkdownTableOptions options) : base(tableRows, options)
    {

    }

    public IEnumerable<MarkdownLine> ToMarkdown()
    {
        var sb = new StringBuilder();
        sb.Append(_columnDivider);
        var numberOfColumns = TableValues.NumberOfColumns;

        for (var i = 0; i < numberOfColumns; i++)
        {
            var columnName = GetColumnName(i, Options.BoldColumnNames);
            var amountOfWhiteSpace = DetermineAmountOfWhiteSpace(columnName, i);
            CreateMarkdownTableCellAsync(sb, columnName, amountOfWhiteSpace);
        }

        return sb
            .ToString()
            .ToMarkdownLine()
            .WrapAsEnumerable();
    }

    private string GetColumnName(int index, bool isBold)
    {
        var columnName = OrderedColumnAttributes.ElementAt(index).Name.Value;

        if (isBold)
        {
            return $"**{columnName}**";
        }

        return columnName;
    }
}
