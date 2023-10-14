using System.Reflection;

namespace MarkdownDocumentBuilder.Extensions;

internal static class ObjectExtensions
{
    public static IEnumerable<T> WrapAsEnumerable<T>(this T item)
    {
        var enumerable = Enumerable.Empty<T>();
        return enumerable.Append(item);
    }
}
