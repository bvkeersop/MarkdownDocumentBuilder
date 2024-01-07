using System.Text;
using MarkdownDocumentBuilder.Model.Document;

namespace MarkdownDocumentBuilder.Model.Elements.Lists;

internal abstract class MarkdownList<TValue> : IMarkdownElement
{
    protected IEnumerable<TValue> _value;
    private readonly IBulletPointProvider _bulletPointProvider;
    private readonly NestedIndex _nestedIndex;

    protected MarkdownList(
        IBulletPointProvider bulletPointProvider,
        params TValue[] value)
    {
        _value = value;
        _bulletPointProvider = bulletPointProvider;
        _nestedIndex = new NestedIndex();
    }

    protected MarkdownList(
       IBulletPointProvider bulletPointProvider,
       IEnumerable<TValue> value)
    {
        _value = (IEnumerable<TValue>) value.ToArray();
        _bulletPointProvider = bulletPointProvider;
        _nestedIndex = new NestedIndex();
    }

    public IEnumerable<MarkdownLine> ToMarkdown()
    {
        var markdownLines = new List<MarkdownLine>();

        foreach (var item in _value)
        {
            if (item is null)
            {
                continue;
            }

            _nestedIndex.Reset();
            var generatedMarkdownLinesForItem = GenerateMarkdownRecursively(item, 0);
            markdownLines.AddRange(generatedMarkdownLinesForItem);
        }

        return markdownLines;
    }

    private IEnumerable<MarkdownLine> GenerateMarkdownRecursively(object item, int indentationLevel)
    {
        if (item is null)
        {
            return Enumerable.Empty<MarkdownLine>();
        }

        var markdownLines = new List<MarkdownLine>();

        var itemType = item.GetType();

        if (ShouldBeRenderedWithToString(item, itemType))
        {
            return RenderWithToString(item, indentationLevel, markdownLines);
        }

        RecursivelyTraversePropertiesUntilCanBeRenderedWithToString(item, indentationLevel, markdownLines, itemType);

        return markdownLines;
    }

    private IEnumerable<MarkdownLine> RenderWithToString(object item, int indentationLevel, List<MarkdownLine> markdownLines)
    {
        var stringRepresentation = item.ToString();

        if (stringRepresentation is null)
        {
            return Enumerable.Empty<MarkdownLine>();
        }

        HandleStringType(markdownLines, stringRepresentation, indentationLevel);
        return markdownLines;
    }

    private void RecursivelyTraversePropertiesUntilCanBeRenderedWithToString(object item, int indentationLevel, List<MarkdownLine> markdownLines, Type itemType)
    {
        var properties = itemType.GetProperties();

        foreach (var property in properties)
        {
            if (property is null)
            {
                continue;
            }

            var propertyValue = property.GetValue(item);

            if (propertyValue is not null)
            {
                RecursivelyTraversePropertyUntilCanBeRenderedWithToString(indentationLevel, markdownLines, propertyValue);
            }
        }
    }

    private void RecursivelyTraversePropertyUntilCanBeRenderedWithToString(
        int indentationLevel, 
        List<MarkdownLine> markdownLines, 
        object propertyValue)
    {
        if (propertyValue is null)
        {
            return;
        }

        // Check string first, as it's more specific than class (string is also regarded a class).
        if (propertyValue is string)
        {
            HandleStringType(markdownLines, propertyValue, indentationLevel);
            return;
        }

        if (propertyValue is IEnumerable<object> enumerable)
        {
            HandleEnumerableType(markdownLines, indentationLevel, enumerable);
            return;
        }

        if (propertyValue.GetType().IsClass)
        {
            HandleClassType(markdownLines, propertyValue, indentationLevel);
            return;
        }

        // Default behavior
        HandleStringType(markdownLines, propertyValue, indentationLevel);
    }

    private void HandleClassType(List<MarkdownLine> markdownLines, object propertyValue, int currentIndentationLevel)
    {
        _nestedIndex.Nest();
        var recursivelyGeneratedMarkdown = GenerateMarkdownRecursively(propertyValue, currentIndentationLevel + 1);
        markdownLines.AddRange(recursivelyGeneratedMarkdown);
        _nestedIndex.Denest();
    }

    private void HandleEnumerableType(List<MarkdownLine> markdownLines, int currentIndentationLevel, IEnumerable<object> enumerable)
    {
        foreach (var item in enumerable)
        {
            if (item is null)
            {
                continue;
            }

            _nestedIndex.Nest();
            var recursivelyGeneratedMarkdown = GenerateMarkdownRecursively(item, currentIndentationLevel + 1);
            markdownLines.AddRange(recursivelyGeneratedMarkdown);
            _nestedIndex.Denest();
        }
    }

    private void HandleStringType(List<MarkdownLine> markdownLines, object propertyValue, int currentIndentationLevel)
    {
        _nestedIndex.Increment();
        var markdownContent = CombineBulletPointAndValue(propertyValue);
        var markdownLine = new MarkdownLine(markdownContent, currentIndentationLevel);
        markdownLines.Add(markdownLine);
    }

    private string CombineBulletPointAndValue(object propertyValue)
    {
        var index = _nestedIndex.Get();
        var bulletPoint = _bulletPointProvider.GetBulletPoint(index);
        var text = propertyValue.ToString();
        return new StringBuilder()
            .Append(bulletPoint)
            .Append(text)
            .ToString();
    }

    private static bool ShouldBeRenderedWithToString(object item, Type itemType)
        => itemType.IsValueType || item is string;

    private static bool IsListType<T>()
    {
        Type type = typeof(T);
        return (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>));
    }
}