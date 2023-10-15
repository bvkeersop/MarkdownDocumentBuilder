using MarkdownDocumentBuilder.Exceptions;
using MarkdownDocumentBuilder.Model.Document;
using MarkdownDocumentBuilder.Model.Elements;

namespace MarkdownDocumentBuilder.Writers;
internal interface IMarkdownDocumentWriter
{
    Task WriteToStreamAsync(MarkdownDocument markdownDocument);
}

internal class MarkdownDocumentWriter : IMarkdownDocumentWriter, IDisposable
{
    private bool _disposedValue;
    private readonly IMarkdownStreamWriter _markdownStreamWriter;

    public MarkdownDocumentWriter(IMarkdownStreamWriter markdownStreamWriter)
    {
        _markdownStreamWriter = markdownStreamWriter;
    }

    public async Task WriteToStreamAsync(MarkdownDocument markdownDocument)
    {
        var markdownElements = markdownDocument.GetMarkdownElements();
        var numberOfMarkdownElements = markdownElements.Count();

        var index = 0;
        foreach (var markdownElement in markdownElements)
        {
            index++;

            var markdownLines = GetMarkdownlinesForElement(markdownElement);

            foreach (var markdownLine in markdownLines)
            {
                await _markdownStreamWriter.WriteLineAsync(markdownLine).ConfigureAwait(false);
            }

            if (!IsLastElement(numberOfMarkdownElements, index))
            {
                await _markdownStreamWriter.WriteNewLineAsync().ConfigureAwait(false);
            }
        }

        await _markdownStreamWriter.FlushAsync().ConfigureAwait(false);
    }

    private static IEnumerable<MarkdownLine> GetMarkdownlinesForElement(IMarkdownElement markdownElement)
    {
        try
        {
            return markdownElement.ToMarkdown();
        }
        catch (Exception ex)
        {
            var elementType = markdownElement.GetType();
            var errorMessage = $"An unexpected error occured while trying to convert {elementType} to markdown";
            throw new MarkdownDocumentBuilderException(MarkdownDocumentBuilderErrorCode.CouldNotConvertToMarkdown, errorMessage, ex);
        }
    }

    private static bool IsLastElement(int numberOfMarkdownElements, int index) => numberOfMarkdownElements <= index;

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                _markdownStreamWriter.Dispose();
            }

            _disposedValue = true;
        }
    }

    ~MarkdownDocumentWriter()
    {
        Dispose(disposing: false);
    }
}