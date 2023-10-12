using DocumentBuilder.Markdown.Constants;
using MarkdownDocumentBuilder.Extensions;
using System.Text;

namespace MarkdownDocumentBuilder.Model.Elements;

internal class HorizontalRule : IMarkdownElement
{
    public IEnumerable<MarkdownLine> ToMarkdown()
        => new StringBuilder()
        .Append(Indicators.HorizontalRule)
        .ToString()
        .ToMarkdownLine()
        .WrapAsEnumerable();
}
