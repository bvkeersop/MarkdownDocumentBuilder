using System.Reflection;

namespace MarkdownDocumentBuilder.Utilities;

internal static class ListReflectionHelper
{
    public static PropertyInfo[] GetPublicProperties<TListItem>()
    {
        Type type = typeof(TListItem);
        return type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
    }
}
