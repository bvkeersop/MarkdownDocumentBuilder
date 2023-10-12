using DocumentBuilder.Options.Enumerations;

namespace MarkdownDocumentBuilder.Options;

public class MarkdownContentOptions
{
    /// <summary>
    /// Options related to table formatting
    /// </summary>
    public MarkdownTableOptions TableOptions { get; set; } = new MarkdownTableOptions();

    /// <summary>
    /// How an list or table will be rendered when it's empty
    /// </summary>
    public NullOrEmptyEnumerableRenderingStrategy NullOrEmptyEnumerableRenderingStrategy { get; set; }
}
