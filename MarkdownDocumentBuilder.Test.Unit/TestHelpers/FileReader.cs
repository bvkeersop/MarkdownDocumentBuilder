namespace MarkdownDocumentBuilder.Test.Unit.TestHelpers;
public class FileReader
{
    public static string ReadFile(string filePath)
    {
        using StreamReader reader = File.OpenText(filePath);
        return reader.ReadToEnd();
    }
}
