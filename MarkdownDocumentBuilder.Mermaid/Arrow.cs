namespace MarkdownDocumentBuilder.Mermaid;

public enum Arrow
{
    Call,
    Return,
    CallAsync,
    ReturnAsync,
}

public static class ArrowExtensions
{
    public static string Translate(this Arrow arrow) => arrow switch
    {
        Arrow.Call => "->>",
        Arrow.Return => "<<--",
        Arrow.CallAsync => 
    }
}
