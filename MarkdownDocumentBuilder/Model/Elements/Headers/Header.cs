using MarkdownDocumentBuilder.Extensions;
using System.Text;

namespace MarkdownDocumentBuilder.Model.Elements.Headers;

internal abstract class Header : IMarkdownElement
{
    private readonly string _indicator;
    public readonly string _value;

    protected Header(string indicator, string value)
    {
        _indicator = indicator;
        _value = value;
    }

    public IEnumerable<MarkdownLine> ToMarkdown()
        => new StringBuilder()
            .Append(_indicator)
            .Append(' ')
            .Append(_value)
            .ToString()
            .ToMarkdownLine()
            .WrapAsEnumerable();
}
