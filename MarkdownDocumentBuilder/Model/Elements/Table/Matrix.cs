using MarkdownDocumentBuilder.Exceptions;
using MarkdownDocumentBuilder.Utilities;
using System.Reflection;

namespace MarkdownDocumentBuilder.Model.Elements.Table;

public class Matrix<TValue>
{
    private TableCell[][] Values { get; }
    public int NumberOfRows { get; }
    public int NumberOfColumns { get; }
    public IEnumerable<TableCell> TableCells
    {
        get { return _tableCells; }
    }

    private readonly Dictionary<int, int> _longestCellSizeOfColumn = new();
    private readonly IEnumerable<TableCell> _tableCells = Enumerable.Empty<TableCell>();

    public Matrix(IEnumerable<TValue> tableRows)
    {
        var orderedPropertyInfos = TableReflectionHelper.GetOrderedTableRowPropertyInfos<TValue>();

        NumberOfRows = tableRows.Count();
        NumberOfColumns = orderedPropertyInfos.Count();

        var matrix = new TableCell[NumberOfRows][];

        for (var rowIndex = 0; rowIndex < NumberOfRows; rowIndex++)
        {
            matrix[rowIndex] = new TableCell[NumberOfColumns];
            for (var columnIndex = 0; columnIndex < NumberOfColumns; columnIndex++)
            {
                var currentProperty = orderedPropertyInfos.ElementAt(columnIndex);
                var currentTableRow = tableRows.ElementAt(rowIndex);

                if (currentTableRow == null)
                {
                    throw new MarkdownDocumentBuilderException(
                        MarkdownDocumentBuilderErrorCode.CouldNotFindTableRowAtIndex,
                        $"Could not find table row at index {rowIndex}");
                }

                var cellValue = GetTableCellValue(currentProperty, currentTableRow);
                var cellType = GetTableCellType(currentProperty);
                var tableCell = new TableCell(cellValue, cellType, rowIndex, columnIndex);
                matrix[rowIndex][columnIndex] = tableCell;
                _tableCells = _tableCells.Append(tableCell);
                CreateOrUpdateLongestCellSizeOfColumn(cellValue, columnIndex);
            }
        }

        Values = matrix;
    }

    private void CreateOrUpdateLongestCellSizeOfColumn(string cellValue, int columnIndex)
    {
        bool entryExists = _longestCellSizeOfColumn.TryGetValue(columnIndex, out var currentLongestSize);

        if (!entryExists)
        {
            _longestCellSizeOfColumn.Add(columnIndex, cellValue.Length);
            return;
        }

        if (cellValue.Length > currentLongestSize)
        {
            _longestCellSizeOfColumn[columnIndex] = cellValue.Length;
        }
    }

    private static string GetTableCellValue(PropertyInfo properyInfo, TValue tableRow)
    {
        return properyInfo.GetValue(tableRow)?.ToString() ?? string.Empty;
    }

    private static Type GetTableCellType(PropertyInfo properyInfo)
    {
        return properyInfo.PropertyType;
    }

    public TableCell[] GetColumn(int index)
    {
        return Values.Select(v => v[index]).ToArray();
    }

    public TableCell[] GetRow(int index)
    {
        return Values[index];
    }

    public TableCell GetValue(int rowIndex, int columnIndex)
    {
        return Values[rowIndex][columnIndex];
    }

    public int GetLongestCellSizeOfColumn(int columnIndex)
    {
        // If there's no longest size cell size, there's no rows, thus the longest size is 0
        if (_longestCellSizeOfColumn.Count == 0)
        {
            return 0;
        }

        if (columnIndex < 0 || columnIndex > NumberOfColumns)
        {
            throw new MarkdownDocumentBuilderException(
                MarkdownDocumentBuilderErrorCode.CouldNotFindColumnAtIndex,
                $"Could not find table row at index {columnIndex}");
        }

        return _longestCellSizeOfColumn[columnIndex];
    }
}
