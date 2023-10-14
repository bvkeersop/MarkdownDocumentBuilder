namespace MarkdownDocumentBuilder.Model.Elements.Headers;

internal class Header1 : Header, IMarkdownElement
{
    public Header1(string value) : base(Indicators.Header1, value)
    {
    }
}
