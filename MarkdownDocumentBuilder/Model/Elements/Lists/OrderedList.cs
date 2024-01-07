using MarkdownDocumentBuilder.Extensions;

namespace MarkdownDocumentBuilder.Model.Elements.Lists;

internal class OrderedList<TValue> : MarkdownList<TValue>
{
    public OrderedList(TValue value) : base(new OrderedBulletPointProvider(), value.WrapAsEnumerable())
    {
    }

    public OrderedList(IEnumerable<TValue> value) : base(new OrderedBulletPointProvider(), value)
    {
    }
}