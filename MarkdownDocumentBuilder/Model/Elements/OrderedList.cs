using MarkdownDocumentBuilder.Extensions;
using System.Text;

namespace MarkdownDocumentBuilder.Model.Elements;

public class OrderedList<TValue> : IMarkdownElement
{
    public IEnumerable<TValue> Value { get; }

    public OrderedList(IEnumerable<TValue> value)
    {
        Value = value;
    }

    public OrderedList(params TValue[] value)
    {
        Value = value;
    }

    public IEnumerable<MarkdownLine> ToMarkdown()
    {
        var markdownLines = new List<MarkdownLine>();
        var index = 1;
        foreach (var item in Value)
        {
            var itemAsmarkdownLine = BuildItem(index, item);
            markdownLines.Add(itemAsmarkdownLine);
            index++;
        }

        return markdownLines;
    }

    private static MarkdownLine BuildItem(int index, TValue item)
    {
        return new StringBuilder()
               .Append(index)
               .Append('.')
               .Append(' ')
               .Append(item)
               .ToString()
               .ToMarkdownLine();
    }
}
