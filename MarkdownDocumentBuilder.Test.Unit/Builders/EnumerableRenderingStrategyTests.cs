using FluentAssertions;
using MarkdownDocumentBuilder.Builders;
using MarkdownDocumentBuilder.Exceptions;
using MarkdownDocumentBuilder.Model.Document.Options;

namespace DocumentBuilder.Test.Unit.Validators;

[TestClass]
public class EnumerableRenderingStrategyTests
{
    [TestMethod]
    public void ShouldRender_EmptyEnumerableAndSkipRender_ReturnsFalse()
    {
        // Arrange
        var emptyEnumerable = Enumerable.Empty<string>();
        var sut = EnumerableRenderingStrategyFactory.Create(NullOrEmptyEnumerableRenderingStrategy.SkipRender);

        // Act
        var shouldRender = sut.ShouldRender(emptyEnumerable);

        // Assert
        shouldRender.Should().BeFalse();
    }

    [TestMethod]
    public void ShouldRender_EmptyEnumerableAndRender_ReturnsTrue()
    {
        // Arrange
        var emptyEnumerable = Enumerable.Empty<string>();
        var sut = EnumerableRenderingStrategyFactory.Create(NullOrEmptyEnumerableRenderingStrategy.Render);

        // Act
        var shouldRender = sut.ShouldRender(emptyEnumerable);

        // Assert
        shouldRender.Should().BeTrue();
    }

    [TestMethod]
    public void ShouldRender_EmptyEnumerableAndThrowException_ThrowsException()
    {
        // Arrange
        var emptyEnumerable = Enumerable.Empty<string>();
        var sut = EnumerableRenderingStrategyFactory.Create(NullOrEmptyEnumerableRenderingStrategy.ThrowException);

        // Act
        var action = () => sut.ShouldRender(emptyEnumerable);

        // Assert
        var exception = action.Should().Throw<MarkdownDocumentBuilderException>()
            .Where(e => e.ErrorCode == MarkdownDocumentBuilderErrorCode.ProvidedEnumerableIsEmpty);
    }
}
