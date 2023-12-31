﻿using MarkdownDocumentBuilder.Exceptions;

namespace MarkdownDocumentBuilder.Builders;

public interface IEnumerableRenderingStrategy
{
    bool ShouldRender<TValue>(IEnumerable<TValue> enumerable);
}

public class ThrowOnNullOrEmptyEnumerableRenderingStrategy : IEnumerableRenderingStrategy
{
    public bool ShouldRender<TValue>(IEnumerable<TValue> enumerable) => enumerable.Any() 
        ? true : throw new MarkdownDocumentBuilderException(MarkdownDocumentBuilderErrorCode.ProvidedEnumerableIsEmpty);
}

public class SkipRenderOnNullOrEmptyRenderingStrategy : IEnumerableRenderingStrategy
{
    public bool ShouldRender<TValue>(IEnumerable<TValue> enumerable) => enumerable.Any();
}

public class AlwaysRenderRenderingStrategy : IEnumerableRenderingStrategy
{
    public bool ShouldRender<TValue>(IEnumerable<TValue> enumerable) => true;
}
