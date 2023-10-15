using MarkdownDocumentBuilder.Model.Document.Options;

namespace MarkdownDocumentBuilder.Writers;

internal static class MarkdownDocumentWriterFactory
{
    public static IMarkdownDocumentWriter Create(
        Stream outputStream,
        IIndentationProvider? indentationProvider = null,
        INewLineProvider? newLineProvider = null)
    {
        indentationProvider ??= IndentationProviderFactory.Create(IndentationType.Spaces, 4);
        newLineProvider ??= NewLineProviderFactory.Create(LineEndings.Environment);
        var streamWriter = new StreamWriter(outputStream);
        var markdownStreamWriter = new MarkdownStreamWriter(streamWriter, indentationProvider, newLineProvider);
        return new MarkdownDocumentWriter(markdownStreamWriter);
    }
}
