using MarkdownDocumentBuilder.Extensions;
using MarkdownDocumentBuilder.Model.Elements.Table.Attributes;
using MarkdownDocumentBuilder.Model.Elements.Table.Options;
using System.Text;

namespace MarkdownDocumentBuilder.Model.Elements.Table;
internal abstract class TableElement<TRow>
{
    protected const char _columnDivider = '|';
    protected const char _whiteSpace = ' ';
    protected const int _minimumNumberOfDividerCharacters = 3;

    public IEnumerable<ColumnAttribute> OrderedColumnAttributes { get; }
    public Matrix<TRow> TableValues { get; }
    public IEnumerable<TableCell> TableCells { get; }
    public MarkdownTableOptions Options { get; }

    protected TableElement(IEnumerable<TRow> tableRows, MarkdownTableOptions options)
    {
        TableValues = new Matrix<TRow>(tableRows);
        OrderedColumnAttributes = TableReflectionHelper.GetOrderedColumnAttributes<TRow>();
        TableCells = CreateEnumerableOfTableCells();
        Options = options;
    }

    protected static string CreateRequiredWhiteSpace(int amountOfWhiteSpace, char whiteSpaceCharacter) => new(whiteSpaceCharacter, amountOfWhiteSpace);

    protected int DetermineAmountOfWhiteSpace(string value, int currentColumnIndex)
    {
        var formatting = Options.Formatting;

        if (formatting == Formatting.AlignColumns)
        {
            var longestColumnCellSize = GetLongestCellSizeForColumn(currentColumnIndex);
            var amountOfCharactersToAdd = CorrectCellSizeBasedOnBoldOption(longestColumnCellSize, value.Length);

            if (longestColumnCellSize < _minimumNumberOfDividerCharacters)
            {
                return _minimumNumberOfDividerCharacters - value.Length;
            }

            return longestColumnCellSize + amountOfCharactersToAdd - value.Length;
        }

        if (formatting == Formatting.None)
        {
            return 0;
        }

        throw new NotSupportedException($"Formatting {Options.Formatting} is not supported");
    }

    protected int GetLongestCellSizeForColumn(int columnIndex)
    {
        if (!Options.BoldColumnNames)
        {
            return GetLongestCellSizeForColumn(columnIndex, Options.BoldColumnNames);
        }

        var longestCellSizeForColumnValue = GetLongestCellSizeForColumn(columnIndex, Options.BoldColumnNames);
        var columnName = OrderedColumnAttributes.ElementAt(columnIndex);
        var boldColumnNameSize = columnName.Name.Value.Length + 4;
        return Math.Max(longestCellSizeForColumnValue, boldColumnNameSize);
    }

    protected int GetLongestCellSizeForColumn(int columnIndex, bool isBold)
    {
        var longestTableValue = TableValues.GetLongestCellSizeOfColumn(columnIndex);
        var columnNameLength = OrderedColumnAttributes.ElementAt(columnIndex).Name.Value.Length;

        if (isBold)
        {
            columnNameLength += 4;
        }

        return Math.Max(longestTableValue, columnNameLength);
    }

    private int CorrectCellSizeBasedOnBoldOption(int cellSize, int longestCellSize)
    {
        // When bold column names is enabled, and the cellsize is shorter than the longest cellsize, we need to align it.
        if (Options.BoldColumnNames && cellSize < longestCellSize)
        {
            return 4;
        }

        // In case of still having the longest cell size, we don't need to add extra space to align it.
        return 0;
    }

    protected static void CreateMarkdownTableCellAsync(StringBuilder sb, string cellValue, int amountOfWhiteSpace, char whiteSpaceCharacter = ' ')
    {
        var whiteSpace = CreateRequiredWhiteSpace(amountOfWhiteSpace, whiteSpaceCharacter);
        sb.Append(_whiteSpace)
            .Append(cellValue)
            .Append(whiteSpace)
            .Append(_whiteSpace)
            .Append(_columnDivider);
    }

    private IEnumerable<TableCell> CreateEnumerableOfTableCells()
    {
        var columnTableCells = Enumerable.Empty<TableCell>();
        for (var i = 0; i < OrderedColumnAttributes.Count(); i++)
        {
            var currentOrderedColumnAttribute = OrderedColumnAttributes.ElementAt(i);
            var tableCell = new TableCell(
                currentOrderedColumnAttribute.Name.Value,
                currentOrderedColumnAttribute.Name.GetType(),
                0,
                i);
            columnTableCells = columnTableCells.Append(tableCell);
        }

        var shiftedTableCells = TableValues.TableCells.Select(t => t.ShiftRow());

        return columnTableCells.Concat(shiftedTableCells);
    }
}
