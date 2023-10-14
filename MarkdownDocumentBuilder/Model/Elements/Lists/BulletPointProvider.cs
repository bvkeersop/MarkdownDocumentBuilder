using System.Text;

namespace MarkdownDocumentBuilder.Model.Elements.Lists;

internal interface IBulletPointProvider
{
    public string GetBulletPoint(int currentIndex);
}

internal class OrderedBulletPointProvider : IBulletPointProvider
{
    public string GetBulletPoint(int currentIndex)
        => new StringBuilder()
            .Append(currentIndex)
            .Append(". ")
            .ToString();
}

internal class UnorderedBulletPointProvider : IBulletPointProvider
{
    public string GetBulletPoint(int currentIndex)
        => new StringBuilder()
            .Append("- ")
            .ToString();
}
