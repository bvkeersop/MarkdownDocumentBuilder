using DocumentBuilder.Options.Enumerations;
using MarkdownDocumentBuilder.Attributes;
using MarkdownDocumentBuilder.Extensions;
using MarkdownDocumentBuilder.Options;
using System.Text;

namespace MarkdownDocumentBuilder.Model.Elements.Table;

internal class TableDivider<TRow> : TableElement<TRow>, IMarkdownElement
{
    private const char _rowDivider = '-';
    private const char _alignmentChar = ':';

    public TableDivider(IEnumerable<TRow> tableRows, MarkdownTableOptions options) : base(tableRows, options)
    {

    }

    public IEnumerable<MarkdownLine> ToMarkdown()
    {
        var sb = new StringBuilder();
        sb.Append(_columnDivider);
        var numberOfColumns = TableValues.NumberOfColumns;

        for (var i = 0; i < numberOfColumns; i++)
        {
            var columnAttribute = OrderedColumnAttributes.ElementAt(i);
            var numberOfDividerCellCharacters = GetNumberOfCharactersForDividerCell(i);
            var divider = new string(_rowDivider, numberOfDividerCellCharacters);
            var alignment = GetAlignment(columnAttribute);
            var alignedDivider = AddAlignment(divider, alignment);
            CreateMarkdownTableCellAsync(sb, alignedDivider, 0, whiteSpaceCharacter: _rowDivider);
        }

        return sb
            .ToString()
            .ToMarkdownLine()
            .WrapAsEnumerable();
    }

    private int GetNumberOfCharactersForDividerCell(int columnIndex)
    {
        var numberOfCharacters = GetLongestCellSizeForColumn(columnIndex, Options.BoldColumnNames);
        if (Options.Formatting == Formatting.None || numberOfCharacters < _minimumNumberOfDividerCharacters)
        {
            return _minimumNumberOfDividerCharacters;
        }
        return numberOfCharacters;
    }

    private Alignment GetAlignment(ColumnAttribute columnAttribute)
    {
        if (columnAttribute.Alignment == Alignment.Default)
        {
            return Options.DefaultAlignment;
        }

        return columnAttribute.Alignment;
    }

    private static string AddAlignment(string cellDividerValue, Alignment alignment)
    {
        if (alignment == Alignment.Left)
        {
            return cellDividerValue.ReplaceAt(0, _alignmentChar);
        }

        if (alignment == Alignment.Right)
        {
            return cellDividerValue.ReplaceAt(cellDividerValue.Length - 1, _alignmentChar);
        }

        if (alignment == Alignment.Center)
        {
            var alignedDivider = cellDividerValue.ReplaceAt(0, _alignmentChar);
            return alignedDivider.ReplaceAt(cellDividerValue.Length - 1, _alignmentChar);
        }

        return cellDividerValue;
    }
}
