namespace MarkdownDocumentBuilder.Model.Elements;

public interface IMarkdownElement
{
    public IEnumerable<MarkdownLine> ToMarkdown();
}
