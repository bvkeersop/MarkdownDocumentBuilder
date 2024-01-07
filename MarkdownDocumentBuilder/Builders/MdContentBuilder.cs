using MarkdownDocumentBuilder.Exceptions;
using MarkdownDocumentBuilder.Extensions;
using MarkdownDocumentBuilder.Model.Document;
using MarkdownDocumentBuilder.Model.Document.Options;
using MarkdownDocumentBuilder.Model.Elements;
using MarkdownDocumentBuilder.Model.Elements.Headers;
using MarkdownDocumentBuilder.Model.Elements.Lists;
using MarkdownDocumentBuilder.Model.Elements.Table;
using MarkdownDocumentBuilder.Model.Elements.Table.Options;

namespace MarkdownDocumentBuilder.Builders;

public class MdContentBuilder : IMarkdownContentBuilder
{
    private readonly MarkdownContent _markdownContent = new();
    private readonly IEnumerableRenderingStrategy _enumerableValidator;
    private readonly MarkdownTableOptions _markdownDocumentOptions;

    public MdContentBuilder(
        MarkdownTableOptions markdownDocumentOptions,
        NullOrEmptyEnumerableRenderingStrategy nullOrEmptyEnumerableRenderingStrategy)
    {
        _enumerableValidator = EnumerableRenderingStrategyFactory.Create(nullOrEmptyEnumerableRenderingStrategy);
        _markdownDocumentOptions = markdownDocumentOptions;
    }

    /// <summary>
    /// Adds a header of type 1 to the document
    /// </summary>
    /// <param name="header1">The value of the header</param>
    /// <returns><see cref="IMarkdownContentBuilder"/></returns>
    public IMarkdownContentBuilder AddHeader1(string header1)
    {
        _ = header1 ?? throw new ArgumentNullException(nameof(header1));
        _markdownContent.AddElement(new Header1(header1));
        return this;
    }

    /// <summary>
    /// Adds a header of type 2 to the document
    /// </summary>
    /// <param name="header2">The value of the header</param>
    /// <returns><see cref="IMarkdownContentBuilder"/></returns>
    public IMarkdownContentBuilder AddHeader2(string header2)
    {
        _ = header2 ?? throw new ArgumentNullException(nameof(header2));
        _markdownContent.AddElement(new Header2(header2));
        return this;
    }

    /// <summary>
    /// Adds a header of type 3 to the document
    /// </summary>
    /// <param name="header3">The value of the header</param>
    /// <returns><see cref="IMarkdownContentBuilder"/></returns>
    public IMarkdownContentBuilder AddHeader3(string header3)
    {
        _ = header3 ?? throw new ArgumentNullException(nameof(header3));
        _markdownContent.AddElement(new Header3(header3));
        return this;
    }

    /// <summary>
    /// Adds a header of type 4 to the document
    /// </summary>
    /// <param name="header4">The value of the header</param>
    /// <returns><see cref="IMarkdownContentBuilder"/></returns>
    public IMarkdownContentBuilder AddHeader4(string header4)
    {
        _ = header4 ?? throw new ArgumentNullException(nameof(header4));
        _markdownContent.AddElement(new Header4(header4));
        return this;
    }

    /// <summary>
    /// Adds a paragraph to the document
    /// </summary>
    /// <param name="lines">The lines of the paragraph, each line will start on a new line</param>
    /// <returns><see cref="IMarkdownContentBuilder"/></returns>
    public IMarkdownContentBuilder AddParagraph(params string[] lines)
    {
        _ = lines ?? throw new ArgumentNullException(nameof(lines));
        _markdownContent.AddElement(new Paragraph(lines));
        return this;
    }

    /// <summary>
    /// Adds an ordered list to the document
    /// </summary>
    /// <param name="classRepresentation">The class representation of the list</param>
    /// <returns><see cref="IMarkdownContentBuilder"/></returns>
    public IMarkdownContentBuilder AddOrderedList<T>(T classRepresentation)
    {
        _ = classRepresentation ?? throw new ArgumentNullException(nameof(classRepresentation));
        _markdownContent.AddElement(new OrderedList<T>(classRepresentation));
        return this;
    }

    /// <summary>
    /// Adds an ordered list to the document
    /// </summary>
    /// <param name="orderedListItems">The items in the ordered list</param>
    /// <returns><see cref="IMarkdownContentBuilder"/></returns>
    public IMarkdownContentBuilder AddOrderedList<T>(IEnumerable<T> orderedListItems)
    {
        _ = orderedListItems ?? throw new ArgumentNullException(nameof(orderedListItems));

        if (!_enumerableValidator.ShouldRender(orderedListItems))
        {
            return this;
        }

        _markdownContent.AddElement(new OrderedList<T>(orderedListItems));
        return this;
    }

    /// <summary>
    /// Adds an ordered list to the document
    /// </summary>
    /// <param name="classRepresentation">The class representation of the list</param>
    /// <returns><see cref="IMarkdownContentBuilder"/></returns>
    public IMarkdownContentBuilder AddUnorderedList<T>(T classRepresentation)
    {
        _ = classRepresentation ?? throw new ArgumentNullException(nameof(classRepresentation));
        _markdownContent.AddElement(new UnorderedList<T>(classRepresentation));
        return this;
    }

    /// <summary>
    /// Adds an ordered list to the document
    /// </summary>
    /// <param name="orderedListItems">The items in the ordered list</param>
    /// <returns><see cref="IMarkdownContentBuilder"/></returns>
    public IMarkdownContentBuilder AddUnorderedList<T>(IEnumerable<T> unorderedListItems)
    {
        _ = unorderedListItems ?? throw new ArgumentNullException(nameof(unorderedListItems));

        if (!_enumerableValidator.ShouldRender(unorderedListItems))
        {
            return this;
        }

        _markdownContent.AddElement(new UnorderedList<T>(unorderedListItems));
        return this;
    }

    /// <summary>
    /// Adds a table to the document
    /// </summary>
    /// <typeparam name="TRow">The type of the row</typeparam>
    /// <param name="tableRows">The values of the table rows</param>
    /// <returns><see cref="IMarkdownContentBuilder"/></returns>
    public IMarkdownContentBuilder AddTable<T>(params T[] tableRows)
    {
        _ = tableRows ?? throw new ArgumentNullException(nameof(tableRows));
        return AddTableInternal(tableRows);
    }

    private IMarkdownContentBuilder AddTableInternal<T>(IEnumerable<T> tableRows)
    {
        if (!_enumerableValidator.ShouldRender(tableRows))
        {
            return this;
        }

        _markdownContent.AddElement(new Table<T>(tableRows, _markdownDocumentOptions));
        return this;
    }

    /// <summary>
    /// Adds an image to the document
    /// </summary>
    /// <param name="name">The name of the image</param>
    /// <param name="path">The path to the image</param>
    /// <param name="caption">The caption of the image</param>
    /// <returns><see cref="IMarkdownContentBuilder"/></returns>
    public IMarkdownContentBuilder AddImage(string name, string path, string? caption = null)
    {
        _ = name ?? throw new ArgumentNullException(nameof(name));
        _ = path ?? throw new ArgumentNullException(nameof(path));
        _markdownContent.AddElement(new Image(name, path, caption));
        return this;
    }

    /// <summary>
    /// Adds a blockquote to the document
    /// </summary>
    /// <param name="quote">The quote</param>
    /// <returns><see cref="IMarkdownContentBuilder"/></returns>
    public IMarkdownContentBuilder AddBlockquote(string quote)
    {
        _ = quote ?? throw new ArgumentNullException(nameof(quote));
        _markdownContent.AddElement(new Blockquote(quote));
        return this;
    }

    /// <summary>
    /// Adds the provided content directly into the document
    /// </summary>
    /// <param name="content">The content</param>
    /// <returns><see cref="IMarkdownContentBuilder"/></returns>
    public IMarkdownContentBuilder AddRaw(string content)
    {
        _ = content ?? throw new ArgumentNullException(nameof(content));
        _markdownContent.AddElement(new Raw(content));
        return this;
    }

    /// <summary>
    /// Adds a codeblock to the document
    /// </summary>
    /// <param name="code">The code inside of the block</param>
    /// <param name="language">The programming language the code is written in</param>
    /// <returns><see cref="IMarkdownContentBuilder"/></returns>
    public IMarkdownContentBuilder AddFencedCodeblock(string code, string? language = null)
    {
        _ = code ?? throw new ArgumentNullException(nameof(code));
        _ = language ?? throw new ArgumentNullException(nameof(language));
        _markdownContent.AddElement(new FencedCodeblock(code, language));
        return this;
    }

    /// <summary>
    /// Adds an horizontal rule
    /// </summary>
    /// <returns><see cref="IMarkdownContentBuilder"/></returns>
    public IMarkdownContentBuilder AddHorizontalRule()
    {
        _markdownContent.AddElement(new HorizontalRule());
        return this;
    }

    /// <summary>
    /// Builds the markdown document
    /// </summary>
    /// <returns>The <see cref="MarkdownContent"/></returns>
    public MarkdownContent Build() => _markdownContent;
}
