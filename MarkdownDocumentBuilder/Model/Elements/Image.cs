using MarkdownDocumentBuilder.Extensions;
using MarkdownDocumentBuilder.Model.Document;
using System.Text;

namespace MarkdownDocumentBuilder.Model.Elements;

internal class Image : IMarkdownElement
{
    public const char Asterix = '*';
    public readonly string _name;
    public readonly string _path;
    public readonly string? _caption;

    public Image(string name, string path, string? caption = null)
    {
        _name = name;
        _path = path;
        _caption = caption;
    }

    public IEnumerable<MarkdownLine> ToMarkdown()
    {
        var markdownLines = new List<MarkdownLine>
        {
            BuildNameWithPath()
        };

        if (_caption != null)
        {
            var caption = BuildCaption();
            markdownLines.Add(caption);
        }

        return markdownLines;
    }

    private MarkdownLine BuildNameWithPath()
        => new StringBuilder().Append("![")
            .Append(_name)
            .Append("](")
            .Append(_path)
            .Append(')')
            .ToString()
            .ToMarkdownLine();

    private MarkdownLine BuildCaption()
          => new StringBuilder()
            .Append(Asterix)
            .Append(_caption)
            .Append(Asterix)
            .ToString()
            .ToMarkdownLine();
}
