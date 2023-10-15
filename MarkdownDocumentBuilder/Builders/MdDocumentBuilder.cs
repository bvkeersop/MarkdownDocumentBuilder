using MarkdownDocumentBuilder.Model.Document;
using MarkdownDocumentBuilder.Model.Document.Options;

namespace MarkdownDocumentBuilder.Builders;

public interface IMarkdownDocumentBuilder
{
    public IMarkdownDocumentBuilder Content(Action<IMarkdownContentBuilder> content, MarkdownContentOptions? options = null);
    public MarkdownDocument Build();
}

public class MdDocumentBuilder : IMarkdownDocumentBuilder // MdDocumentBuilder because of project name
{
    private readonly MarkdownDocument _markdownDocument = MarkdownDocument.Init();

    public IMarkdownDocumentBuilder Content(Action<IMarkdownContentBuilder> content, MarkdownContentOptions? options = null)
    {
        options ??= new MarkdownContentOptions();
        var builder = new MdContentBuilder(options.TableOptions, options.NullOrEmptyEnumerableRenderingStrategy);
        content(builder);
        var buildContent = builder.Build();
        _markdownDocument.Content = buildContent;
        return this;
    }

    public MarkdownDocument Build() => _markdownDocument;
}
