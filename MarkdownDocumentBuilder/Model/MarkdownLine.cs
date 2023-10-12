namespace MarkdownDocumentBuilder.Model;

public record MarkdownLine
{
    public string? Content { get; init; } = string.Empty;
    public int IndentationLevel { get; set; }

    public MarkdownLine(string? content = null, int indentationLevel = 0)
    {
        IndentationLevel = indentationLevel;
        Content = content;
    }

    public static MarkdownLine Empty() => new();
}
