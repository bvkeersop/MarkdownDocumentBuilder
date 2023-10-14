﻿namespace MarkdownDocumentBuilder.Model.Elements;

internal class FencedCodeblock : IMarkdownElement
{
    private readonly string _codeblock;
    private readonly string? _language;

    public FencedCodeblock(string codeblock, string? language = null)
    {
        _codeblock = codeblock;
        _language = language;
    }

    public IEnumerable<MarkdownLine> ToMarkdown() => new List<MarkdownLine>
    {
        new MarkdownLine(Indicators.Codeblock),
        new MarkdownLine(_language),
        MarkdownLine.Empty(),
        new MarkdownLine(_codeblock),
        MarkdownLine.Empty(),
        new MarkdownLine(Indicators.Codeblock)
    };
}
