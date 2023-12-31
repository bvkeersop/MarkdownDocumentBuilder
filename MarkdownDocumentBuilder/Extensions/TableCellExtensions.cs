﻿using MarkdownDocumentBuilder.Model.Elements.Table;

namespace MarkdownDocumentBuilder.Extensions;

public static class TableCellExtensions
{
    public static TableCell ShiftRow(this TableCell tableCell, int amountOfRows = 1)
    {
        var newRowPosition = tableCell.RowPosition + amountOfRows;
        return new TableCell(tableCell.Value, tableCell.Type, newRowPosition, tableCell.ColumnPosition);
    }
}
