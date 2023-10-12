using MarkdownDocumentBuilder.Model;
using MarkdownDocumentBuilder.Utilities;

namespace MarkdownDocumentBuilder.Writers;
public interface IMarkdownStreamWriter : IDisposable
{
    Task WriteAsync(char value);
    Task WriteAsync(string value);
    Task WriteLineAsync(MarkdownLine line);
    Task WriteNewLineAsync();
    Task FlushAsync();
}

internal class MarkdownStreamWriter : IMarkdownStreamWriter
{
    private bool _disposedValue;

    public StreamWriter StreamWriter { get; }
    public INewLineProvider NewLineProvider { get; }

    public MarkdownStreamWriter(StreamWriter streamWriter, INewLineProvider newLineProvider)
    {
        StreamWriter = streamWriter;
        NewLineProvider = newLineProvider;
    }

    public async Task WriteAsync(char value)
    {
        await StreamWriter.WriteAsync(value).ConfigureAwait(false);
    }

    public async Task WriteAsync(string value)
    {
        await StreamWriter.WriteAsync(value).ConfigureAwait(false);
    }

    public async Task WriteLineAsync(MarkdownLine line)
    {
        await StreamWriter.WriteAsync(line.Content).ConfigureAwait(false);
        await WriteNewLineAsync().ConfigureAwait(false);
    }

    public async Task WriteNewLineAsync()
    {
        await StreamWriter.WriteAsync(NewLineProvider.GetNewLine()).ConfigureAwait(false);
    }

    public async Task FlushAsync()
    {
        await StreamWriter.FlushAsync();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                StreamWriter.Dispose();
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

