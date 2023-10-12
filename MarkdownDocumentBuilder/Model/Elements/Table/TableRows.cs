using MarkdownDocumentBuilder.Extensions;
using MarkdownDocumentBuilder.Options;
using System.Text;

namespace MarkdownDocumentBuilder.Model.Elements.Table;
internal class TableRows<TRow> : TableElement<TRow>, IMarkdownElement
{
    public TableRows(IEnumerable<TRow> tableRows, MarkdownTableOptions options) : base(tableRows, options)
    {

    }

    public IEnumerable<MarkdownLine> ToMarkdown()
    {
        var markdownLines = new List<MarkdownLine>();
        var sb = new StringBuilder();
        var numberOfRows = TableValues.NumberOfRows;

        for (var i = 0; i < numberOfRows; i++)
        {
            var currentRow = TableValues.GetRow(i);
            var markdownLine = CreateMarkdownTableRowAsync(sb, currentRow);
            markdownLines.Add(markdownLine);
        }

        return markdownLines;
    }

    private MarkdownLine CreateMarkdownTableRowAsync(StringBuilder sb, TableCell[] tableRow)
    {
        sb.Append(_columnDivider);
        for (var i = 0; i < tableRow.Length; i++)
        {
            var cellValue = tableRow[i].Value;
            var amountOfWhiteSpace = DetermineAmountOfWhiteSpace(cellValue, i);
            CreateMarkdownTableCellAsync(sb, cellValue, amountOfWhiteSpace);
        }
        return sb.ToString().ToMarkdownLine();
    }
}
