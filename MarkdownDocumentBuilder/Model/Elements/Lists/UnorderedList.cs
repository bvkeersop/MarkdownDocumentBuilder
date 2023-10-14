namespace MarkdownDocumentBuilder.Model.Elements.Lists;

internal class UnorderedList<TValue> : MarkdownList<TValue>
{
    public UnorderedList(params TValue[] value) : base(new UnorderedBulletPointProvider(), value)
    {
    }
}
