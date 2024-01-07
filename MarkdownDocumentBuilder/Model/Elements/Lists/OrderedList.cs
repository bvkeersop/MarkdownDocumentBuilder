namespace MarkdownDocumentBuilder.Model.Elements.Lists;

internal class OrderedList<TValue> : MarkdownList<TValue>
{
    public OrderedList(params TValue[] value) : base(new OrderedBulletPointProvider(), value)
    {
    }

    public OrderedList(IEnumerable<TValue> value) : base(new OrderedBulletPointProvider(), value)
    {
    }
}