using System.Text;

namespace MarkdownDocumentBuilder.Test.Unit.TestHelpers;

internal static class StreamExtensions
{
    public static string ReadAsString(this Stream stream)
    {
        stream.Position = 0;
        using StreamReader reader = new(stream, Encoding.UTF8);
        return reader.ReadToEnd();
    }
}
