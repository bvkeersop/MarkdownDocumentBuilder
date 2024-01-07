using MarkdownDocumentBuilder.Extensions;

namespace MarkdownDocumentBuilder.Model.Elements.Lists;

internal class UnorderedList<TValue> : MarkdownList<TValue>
{
    public UnorderedList(TValue value) : base(new OrderedBulletPointProvider(), value.WrapAsEnumerable())
    {
    }

    public UnorderedList(IEnumerable<TValue> value) : base(new OrderedBulletPointProvider(), value)
    {
    }
}
