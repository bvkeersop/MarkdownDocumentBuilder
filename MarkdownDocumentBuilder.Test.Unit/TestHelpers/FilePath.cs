namespace MarkdownDocumentBuilder.Test.Unit.TestHelpers;

internal static class FilePath
{
    public static string TestPath = "../../../TestOutput/test-output.md";
    public static string Combine(string directory, string fileName) 
        => Path.Combine("Resources", directory, fileName);
}
