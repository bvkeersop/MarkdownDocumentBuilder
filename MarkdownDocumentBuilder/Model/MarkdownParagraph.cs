using MarkdownDocumentBuilder.Model.Elements;

namespace MarkdownDocumentBuilder.Model;

internal class MarkdownParagraph
{
    public IList<IMarkdownElement> Elements { get; private set; }

    public MarkdownParagraph()
    {
        Elements = new List<IMarkdownElement>();
    }

    public void AddElement(IMarkdownElement element) => Elements.Add(element);
}
