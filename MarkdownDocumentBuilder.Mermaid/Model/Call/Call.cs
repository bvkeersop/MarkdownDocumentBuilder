using MarkdownDocumentBuilder.Shared;
using System.Text;

namespace MarkdownDocumentBuilder.Mermaid.Model.Call;

internal abstract class Call : IMarkdownElement
{
    private readonly string _caller;
    private readonly Arrow _arrow;
    private readonly string _callee;

    public Call(string caller, Arrow arrow, string callee)
    {
        _caller = caller;
        _arrow = arrow;
        _callee = callee;
    }

    public IEnumerable<MarkdownLine> ToMarkdown()
        => new StringBuilder()
        .Append(_caller)
        .Append(_arrow)
        .Append(_callee)
        .
}
