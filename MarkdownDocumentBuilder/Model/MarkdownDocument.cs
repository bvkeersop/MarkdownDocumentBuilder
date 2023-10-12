using MarkdownDocumentBuilder.Builders;
using MarkdownDocumentBuilder.Factories;
using MarkdownDocumentBuilder.Model.Elements;
using MarkdownDocumentBuilder.Options;

namespace MarkdownDocumentBuilder.Model;

public class MarkdownDocument
{
    public MarkdownContent Content { get; set; } = new();

    private readonly MarkdownDocumentOptions _options;
    private readonly Action<IMarkdownDocumentBuilder> _executeBuildSteps;

    public IEnumerable<IMarkdownElement> GetMarkdownElements() => Content.Elements;

    public static MarkdownDocument Init()
    {
        var document = (IMarkdownDocumentBuilder document) => { };
        return new MarkdownDocument(document);
    }

    public static MarkdownDocument Build(
        Action<IMarkdownDocumentBuilder> document,
        MarkdownDocumentOptions? options = null) => new(document, options);

    protected MarkdownDocument(
       Action<IMarkdownDocumentBuilder> document,
       MarkdownDocumentOptions? options = null)
    {
        _executeBuildSteps = document;
        _options = options ?? new MarkdownDocumentOptions();
    }


    /// <summary>
    /// Writes the document to the provided path, will replace existing documents
    /// </summary>
    /// <param name="filePath">The path which the file should be written to</param>
    /// <returns><see cref="Task"/></returns>
    public Task SaveAsync(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException("filePath cannot be null or empty");
        }

        return BuildInternalAsync(filePath);
    }

    /// <summary>
    /// Writes the document to the provided output stream
    /// </summary>
    /// <param name="outputStream">The stream to write to</param>
    /// <returns><see cref="Task"/></returns>
    public Task SaveAsync(Stream outputStream)
    {
        _ = outputStream ?? throw new ArgumentNullException(nameof(outputStream));
        return SaveInternalAsync(outputStream);
    }

    private async Task SaveInternalAsync(Stream outputStream)
    {
        var documentBuilder = new Builders.MdDocumentBuilder();
        _executeBuildSteps(documentBuilder);
        var markdownDocument = documentBuilder.Build();
        var markdownDocumentWriter = MarkdownDocumentWriterFactory.Create(outputStream, _options.NewLineProvider);
        await markdownDocumentWriter.WriteToStreamAsync(markdownDocument).ConfigureAwait(false);
    }

    private async Task<FileStream> BuildInternalAsync(string filePath)
    {
        FileStream fileStream = File.Create(filePath);
        await SaveAsync(fileStream).ConfigureAwait(false);
        return fileStream;
    }
}
