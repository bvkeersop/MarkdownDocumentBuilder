using MarkdownDocumentBuilder.Extensions;

namespace MarkdownDocumentBuilder.Model.Elements;

internal class Raw : IMarkdownElement
{
    public string Value { get; }

    public Raw(string value)
    {
        Value = value;
    }

    public IEnumerable<MarkdownLine> ToMarkdown()
        => new MarkdownLine(Value)
        .WrapAsEnumerable();
}
