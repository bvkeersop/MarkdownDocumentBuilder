using MarkdownDocumentBuilder.Extensions;
using System.Text;

namespace MarkdownDocumentBuilder.Model.Elements;

internal class Image : IMarkdownElement
{
    public const char Asterix = '*';
    public string Name { get; }
    public string Path { get; }
    public string? Caption { get; }

    public Image(string name, string path, string? caption = null)
    {
        Name = name;
        Path = path;
        Caption = caption;
    }

    public IEnumerable<MarkdownLine> ToMarkdown()
    {
        var markdownLines = new List<MarkdownLine>
        {
            BuildNameWithPath()
        };

        if (Caption != null)
        {
            var caption = BuildCaption();
            markdownLines.Add(caption);
        }

        return markdownLines;
    }

    private MarkdownLine BuildNameWithPath()
        => new StringBuilder().Append("![")
            .Append(Name)
            .Append("](")
            .Append(Path)
            .Append(')')
            .ToString()
            .ToMarkdownLine();

    private MarkdownLine BuildCaption()
          => new StringBuilder()
            .Append(Asterix)
            .Append(Caption)
            .Append(Asterix)
            .ToString()
            .ToMarkdownLine();
}
