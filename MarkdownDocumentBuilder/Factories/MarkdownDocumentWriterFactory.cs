using MarkdownDocumentBuilder.Options.Enums;
using MarkdownDocumentBuilder.Utilities;
using MarkdownDocumentBuilder.Writers;

namespace MarkdownDocumentBuilder.Factories;

internal static class MarkdownDocumentWriterFactory
{
    public static IMarkdownDocumentWriter Create(
        Stream outputStream,
        IIndentationProvider indentationProvider,
        INewLineProvider? newLineProvider = null)
    {
        newLineProvider ??= NewLineProviderFactory.Create(LineEndings.Environment);
        var streamWriter = new StreamWriter(outputStream);
        var markdownStreamWriter = new MarkdownStreamWriter(streamWriter, indentationProvider, newLineProvider);
        return new MarkdownDocumentWriter(markdownStreamWriter);
    }
}
