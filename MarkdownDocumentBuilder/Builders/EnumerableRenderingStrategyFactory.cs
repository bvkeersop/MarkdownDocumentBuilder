using MarkdownDocumentBuilder.Model.Document.Options;

namespace MarkdownDocumentBuilder.Builders;

public static class EnumerableRenderingStrategyFactory
{
    public static IEnumerableRenderingStrategy Create(NullOrEmptyEnumerableRenderingStrategy strategy) => strategy switch
    {
        NullOrEmptyEnumerableRenderingStrategy.SkipRender => new SkipRenderOnNullOrEmptyRenderingStrategy(),
        NullOrEmptyEnumerableRenderingStrategy.Render => new AlwaysRenderRenderingStrategy(),
        NullOrEmptyEnumerableRenderingStrategy.ThrowException => new ThrowOnNullOrEmptyEnumerableRenderingStrategy(),
        _ => throw new NotSupportedException($"{strategy} is currently not supported")
    };
}
