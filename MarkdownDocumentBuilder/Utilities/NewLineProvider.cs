namespace MarkdownDocumentBuilder.Utilities;

public interface INewLineProvider
{
    string GetNewLine();
}

internal class WindowsNewLineProvider : INewLineProvider
{
    public string GetNewLine()
    {
        return "\r\n";
    }
}

internal class LinuxNewLineProvider : INewLineProvider
{
    public string GetNewLine()
    {
        return "\n";
    }
}

internal class EnvironmentNewLineProvider : INewLineProvider
{
    public string GetNewLine()
    {
        return Environment.NewLine;
    }
}
