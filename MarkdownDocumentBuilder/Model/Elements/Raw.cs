using MarkdownDocumentBuilder.Extensions;
using MarkdownDocumentBuilder.Model.Document;

namespace MarkdownDocumentBuilder.Model.Elements;

internal class Raw : IMarkdownElement
{
    public readonly string _value;

    public Raw(string value)
    {
        _value = value;
    }

    public IEnumerable<MarkdownLine> ToMarkdown()
        => new MarkdownLine(_value)
        .WrapAsEnumerable();
}
