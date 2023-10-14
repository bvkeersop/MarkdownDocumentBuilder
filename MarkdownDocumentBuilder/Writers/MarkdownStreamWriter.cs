using MarkdownDocumentBuilder.Model;
using MarkdownDocumentBuilder.Utilities;

namespace MarkdownDocumentBuilder.Writers;
internal interface IMarkdownStreamWriter : IDisposable
{
    Task WriteLineAsync(MarkdownLine line);
    Task WriteNewLineAsync();
    Task FlushAsync();
}

internal class MarkdownStreamWriter : IMarkdownStreamWriter
{
    private bool _disposedValue;
    private readonly StreamWriter _streamWriter;
    private readonly INewLineProvider _newLineProvider;
    private readonly IIndentationProvider _indentationProvider;

    public MarkdownStreamWriter(
        StreamWriter streamWriter,
        IIndentationProvider indentationProvider,
        INewLineProvider newLineProvider)
    {
        _streamWriter = streamWriter;
        _newLineProvider = newLineProvider;
        _indentationProvider = indentationProvider;
    }

    public async Task WriteLineAsync(MarkdownLine line)
    {
        var indentedMarkdownLineAsText = line.GetIndentedContent(_indentationProvider);
        await _streamWriter.WriteAsync(indentedMarkdownLineAsText).ConfigureAwait(false);
        await WriteNewLineAsync().ConfigureAwait(false);
    }

    public async Task WriteNewLineAsync()
    {
        await _streamWriter.WriteAsync(_newLineProvider.GetNewLine()).ConfigureAwait(false);
    }

    public async Task FlushAsync()
    {
        await _streamWriter.FlushAsync();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                _streamWriter.Dispose();
            }

            _disposedValue = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    ~MarkdownStreamWriter()
    {
        Dispose(disposing: false);
    }
}

