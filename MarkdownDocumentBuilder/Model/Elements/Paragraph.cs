namespace MarkdownDocumentBuilder.Model.Elements;

internal class Paragraph : IMarkdownElement
{
    public readonly string[] _lines;

    public Paragraph(params string[] lines)
    {
        _lines = lines;
    }

    public IEnumerable<MarkdownLine> ToMarkdown() 
        => _lines.Select(line => new MarkdownLine(line));
}
