using MarkdownDocumentBuilder.Model.Elements;

namespace MarkdownDocumentBuilder.Model;

public class MarkdownContent
{
    public IList<IMarkdownElement> Elements { get; private set; }

    public MarkdownContent()
    {
        Elements = new List<IMarkdownElement>();
    }

    public void AddElement(IMarkdownElement element) => Elements.Add(element);
}
