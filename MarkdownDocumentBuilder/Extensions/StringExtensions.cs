using MarkdownDocumentBuilder.Model.Document;

namespace MarkdownDocumentBuilder.Extensions;

internal static class StringExtensions
{
    public static MarkdownLine ToMarkdownLine(this string content, int indentationLevel = 0) => new(content, indentationLevel);
    public static string ReplaceAt(this string @string, int index, char replacementCharacter)
        => @string.Remove(index, 1).Insert(index, replacementCharacter.ToString());
}
