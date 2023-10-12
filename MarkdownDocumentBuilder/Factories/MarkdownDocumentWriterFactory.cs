using MarkdownDocumentBuilder.Utilities;
using MarkdownDocumentBuilder.Writers;

namespace MarkdownDocumentBuilder.Factories;

public static class MarkdownDocumentWriterFactory
{
    public static IMarkdownDocumentWriter Create(Stream outputStream, INewLineProvider newLineProvider)
    {
        var streamWriter = new StreamWriter(outputStream);
        var markdownStreamWriter = new MarkdownStreamWriter(streamWriter, newLineProvider);
        return new MarkdownDocumentWriter(markdownStreamWriter);
    }
}
