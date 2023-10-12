using DocumentBuilder.Markdown.Constants;

namespace MarkdownDocumentBuilder.Model.Elements;

internal class Header1 : Header, IMarkdownElement
{
    public Header1(string value) : base(Indicators.Header1, value)
    {
    }
}
