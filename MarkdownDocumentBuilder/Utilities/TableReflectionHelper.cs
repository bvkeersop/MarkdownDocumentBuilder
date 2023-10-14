using MarkdownDocumentBuilder.Attributes;
using System.Reflection;

namespace MarkdownDocumentBuilder.Utilities;

internal static class TableReflectionHelper
{
    public static IOrderedEnumerable<PropertyInfo> GetOrderedTableRowPropertyInfos<TRow>()
    {
        var tableRowProperties = GetTableRowPropertyInfos<TRow>();
        var filteredTableRowProperties = FilterPropertiesWithIgnoreColumnAttribute<TRow>(tableRowProperties);
        return filteredTableRowProperties.OrderBy(t => GetColumnAttribute(t).Order);
    }

    public static IEnumerable<ColumnAttribute> GetOrderedColumnAttributes<TRow>()
    {
        var tableRowProperties = GetTableRowPropertyInfos<TRow>();
        var filteredTableRowProperties = FilterPropertiesWithIgnoreColumnAttribute<TRow>(tableRowProperties);

        return filteredTableRowProperties
            .Select(t => GetColumnAttribute(t))
            .OrderBy(t => t.Order);
    }

    private static IEnumerable<PropertyInfo> FilterPropertiesWithIgnoreColumnAttribute<TRow>(IEnumerable<PropertyInfo> tableRowProperties)
    {
        return tableRowProperties.Where(t => !HasIgnoreAttribute(t));
    }

    private static IEnumerable<PropertyInfo> GetTableRowPropertyInfos<TRow>()
    {
        var tableRowType = typeof(TRow);
        return tableRowType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
    }

    private static ColumnAttribute GetColumnAttribute(PropertyInfo tableCell)
    {
        var columnAttribute = tableCell.GetCustomAttribute<ColumnAttribute>();

        if (columnAttribute is null)
        {
            return new ColumnAttribute(tableCell.Name);
        }

        if (!columnAttribute.Name.IsSet)
        {
            return new ColumnAttribute(tableCell.Name, columnAttribute.Alignment, columnAttribute.Order);
        }

        return columnAttribute;
    }

    private static bool HasIgnoreAttribute(PropertyInfo tableCell)
    {
        var columnAttribute = tableCell.GetCustomAttribute<IgnoreColumnAttribute>();

        if (columnAttribute is null)
        {
            return false;
        }

        return true;
    }
}
