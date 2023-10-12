using DocumentBuilder.Markdown.Constants;

namespace MarkdownDocumentBuilder.Model.Elements;
internal class Header4 : Header, IMarkdownElement
{
    public Header4(string value) : base(Indicators.Header4, value)
    {
    }
}