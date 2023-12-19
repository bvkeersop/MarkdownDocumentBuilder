using System.Text;

namespace MarkdownDocumentBuilder.Shared;

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

    public string GetIndentedContent(IIndentationProvider indentationProvider)
    {
        var indentation = indentationProvider.GetIndentation(IndentationLevel);

        if (string.IsNullOrEmpty(Content))
        {
            return string.Empty;
        }

        return new StringBuilder()
            .Append(indentation)
            .Append(Content)
            .ToString();
    }
}
