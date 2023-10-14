using MarkdownDocumentBuilder.Extensions;
using System.Text;

namespace MarkdownDocumentBuilder.Model.Elements;

internal class Blockquote : IMarkdownElement
{
    private readonly string _value;

    public Blockquote(string value)
    {
        _value = value;
    }

    public IEnumerable<MarkdownLine> ToMarkdown()
        => new StringBuilder()
        .Append(Indicators.Blockquote)
        .Append(' ')
        .Append(_value)
        .ToString()
        .ToMarkdownLine()
        .WrapAsEnumerable();
}
