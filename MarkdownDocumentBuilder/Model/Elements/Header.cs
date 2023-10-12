using MarkdownDocumentBuilder.Extensions;
using MarkdownDocumentBuilder.Options;
using System.Text;

namespace MarkdownDocumentBuilder.Model.Elements;

internal abstract class Header : IMarkdownElement
{
    public string Indicator { get; }
    public string Value { get; }

    protected Header(string indicator, string value)
    {
        Indicator = indicator;
        Value = value;
    }

    public IEnumerable<MarkdownLine> ToMarkdown()
        => new StringBuilder()
            .Append(Indicator)
            .Append(' ')
            .Append(Value)
            .ToString()
            .ToMarkdownLine()
            .WrapAsEnumerable();
}
