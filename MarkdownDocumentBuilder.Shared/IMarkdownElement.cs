namespace MarkdownDocumentBuilder.Shared;

public interface IMarkdownElement
{
    public IEnumerable<MarkdownLine> ToMarkdown();
}
