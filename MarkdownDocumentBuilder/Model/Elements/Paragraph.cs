namespace MarkdownDocumentBuilder.Model.Elements;

public class Paragraph : IMarkdownElement
{
    public string[] Lines { get; }

    public Paragraph(params string[] lines)
    {
        Lines = lines;
    }

    public IEnumerable<MarkdownLine> ToMarkdown() => Lines.Select(line => new MarkdownLine(line));
}
