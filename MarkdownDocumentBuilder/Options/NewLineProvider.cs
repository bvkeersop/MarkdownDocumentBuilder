namespace MarkdownDocumentBuilder.Options;

public interface INewLineProvider
{
    string GetNewLine();
}

public class WindowsNewLineProvider : INewLineProvider
{
    public string GetNewLine() => "\r\n";
}

public class LinuxNewLineProvider : INewLineProvider
{
    public string GetNewLine() => "\n";
}

public class EnvironmentNewLineProvider : INewLineProvider
{
    public string GetNewLine() => Environment.NewLine;
}
