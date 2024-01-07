using MarkdownDocumentBuilder.Abstractions;
using MarkdownDocumentBuilder.Model.Document;

namespace MarkdownDocumentBuilder.Builders;

public interface IMarkdownContentBuilder
{
    /// <summary>
    /// Adds a header of type 1 to the document
    /// </summary>
    /// <param name="header1">The value of the header</param>
    /// <returns><see cref="IMarkdownContentBuilder"/></returns>
    IMarkdownContentBuilder AddHeader1(string header1);

    /// <summary>
    /// Adds a header of type 2 to the document
    /// </summary>
    /// <param name="header2">The value of the header</param>
    /// <returns><see cref="IMarkdownContentBuilder"/></returns>
    IMarkdownContentBuilder AddHeader2(string header2);

    /// <summary>
    /// Adds a header of type 3 to the document
    /// </summary>
    /// <param name="header3">The value of the header</param>
    /// <returns><see cref="IMarkdownContentBuilder"/></returns>
    IMarkdownContentBuilder AddHeader3(string header3);

    /// <summary>
    /// Adds a header of type 4 to the document
    /// </summary>
    /// <param name="header4">The value of the header</param>
    /// <returns><see cref="IMarkdownContentBuilder"/></returns>
    IMarkdownContentBuilder AddHeader4(string header4);

    /// <summary>
    /// Adds a paragraph to the document
    /// </summary>
    /// <param name="lines">The lines of the paragraph, each line will start on a new line</param>
    /// <returns><see cref="IMarkdownContentBuilder"/></returns>
    IMarkdownContentBuilder AddParagraph(params string[] lines);

    /// <summary>
    /// Adds an ordered list to the document
    /// </summary>
    /// <param name="classRepresentation">The class representation of the list</param>
    /// <returns><see cref="IMarkdownContentBuilder"/></returns>

    IMarkdownContentBuilder AddOrderedList(IListRepresentation classRepresentation);

    /// <summary>
    /// Adds an ordered list to the document
    /// </summary>
    /// <param name="orderedListItems">The items in the ordered list</param>
    /// <returns><see cref="IMarkdownContentBuilder"/></returns>
    IMarkdownContentBuilder AddOrderedList<T>(IEnumerable<T> orderedListItems);

    /// <summary>
    /// Adds an unordered list to the document
    /// </summary>
    /// <param name="unorderedListItems">The items in the unordered list</param>
    /// <returns><see cref="IMarkdownContentBuilder"/></returns>
    IMarkdownContentBuilder AddUnorderedList<T>(IEnumerable<T> unorderedListItems);

    /// <summary>
    /// Adds an ordered list to the document
    /// </summary>
    /// <param name="classRepresentation">The class representation of the list</param>
    /// <returns><see cref="IMarkdownContentBuilder"/></returns>
    IMarkdownContentBuilder AddUnorderedList<T>(T classRepresentation);

    /// <summary>
    /// Adds a table to the document
    /// </summary>
    /// <typeparam name="TRow">The type of the row</typeparam>
    /// <param name="tableRows">The values of the table rows</param>
    /// <returns><see cref="IMarkdownContentBuilder"/></returns>
    IMarkdownContentBuilder AddTable<T>(params T[] tableRows);

    /// <summary>
    /// Adds an image to the document
    /// </summary>
    /// <param name="name">The name of the image</param>
    /// <param name="path">The path to the image</param>
    /// <param name="caption">The caption of the image</param>
    /// <returns><see cref="IMarkdownContentBuilder"/></returns>
    IMarkdownContentBuilder AddImage(string name, string path, string? caption = null);

    /// <summary>
    /// Adds a blockquote to the document
    /// </summary>
    /// <param name="quote">The quote</param>
    /// <returns><see cref="IMarkdownContentBuilder"/></returns>
    IMarkdownContentBuilder AddBlockquote(string quote);

    /// <summary>
    /// Adds the provided content directly into the document
    /// </summary>
    /// <param name="content">The content</param>
    /// <returns><see cref="IMarkdownContentBuilder"/></returns>
    IMarkdownContentBuilder AddRaw(string content);

    /// <summary>
    /// Adds a codeblock to the document
    /// </summary>
    /// <param name="code">The code inside of the block</param>
    /// <param name="language">The programming language the code is written in</param>
    /// <returns><see cref="IMarkdownContentBuilder"/></returns>
    IMarkdownContentBuilder AddFencedCodeblock(string code, string? language = null);

    /// <summary>
    /// Adds an horizontal rule
    /// </summary>
    /// <returns><see cref="IMarkdownContentBuilder"/></returns>
    IMarkdownContentBuilder AddHorizontalRule();

    /// <summary>
    /// Builds the markdown document
    /// </summary>
    /// <returns>The <see cref="MarkdownContent"/></returns>
    public MarkdownContent Build();
}
