using DocumentBuilder.Markdown.Constants;
using MarkdownDocumentBuilder.Extensions;
using System.Text;

namespace MarkdownDocumentBuilder.Model.Elements;

internal class Blockquote : IMarkdownElement
{
    private string Value { get; }

    public Blockquote(string value)
    {
        Value = value;
    }

    public IEnumerable<MarkdownLine> ToMarkdown()
        => new StringBuilder()
        .Append(Indicators.Blockquote)
        .Append(' ')
        .Append(Value)
        .ToString()
        .ToMarkdownLine()
        .WrapAsEnumerable();
}
