using DocumentBuilder.Markdown.Constants;

namespace MarkdownDocumentBuilder.Model.Elements;
internal class Header2 : Header, IMarkdownElement
{
    public Header2(string value) : base(Indicators.Header2, value)
    {
    }
}
