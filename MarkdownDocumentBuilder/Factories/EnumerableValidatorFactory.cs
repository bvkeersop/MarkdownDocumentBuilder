﻿using DocumentBuilder.Core.Utilities;
using DocumentBuilder.Options.Enumerations;

namespace DocumentBuilder.Factories;

public static class EnumerableValidatorFactory
{
    public static IEnumerableRenderingStrategy Create(NullOrEmptyEnumerableRenderingStrategy strategy) => strategy switch
    {
        NullOrEmptyEnumerableRenderingStrategy.SkipRender => new SkipRenderOnNullOrEmptyRenderingStrategy(),
        NullOrEmptyEnumerableRenderingStrategy.Render => new AlwaysRenderRenderingStrategy(),
        NullOrEmptyEnumerableRenderingStrategy.ThrowException => new ThrowOnNullOrEmptyEnumerableRenderingStrategy(),
        _ => throw new NotSupportedException($"{strategy} is currently not supported")
    };
}