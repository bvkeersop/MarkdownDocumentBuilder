using System.Text;
using DocumentBuilder.Markdown.Constants;
using MarkdownDocumentBuilder.Extensions;

namespace MarkdownDocumentBuilder.Model.Elements;

public class UnorderedList<TValue> : IMarkdownElement
{
    public IEnumerable<TValue> Value { get; }

    public UnorderedList(IEnumerable<TValue> value)
    {
        Value = value;
    }

    public IEnumerable<MarkdownLine> ToMarkdown()
    {
        var markdownLines = new List<MarkdownLine>();
        foreach (var item in Value)
        {
            var itemAsmarkdownLine = BuildItem(item);
            markdownLines.Add(itemAsmarkdownLine);
        }

        return markdownLines;
    }

    private static MarkdownLine BuildItem(TValue item)
    {
        return new StringBuilder()
               .Append(Indicators.UnorderedListItem)
               .Append(' ')
               .Append(item)
               .ToString()
               .ToMarkdownLine();
    }
}
