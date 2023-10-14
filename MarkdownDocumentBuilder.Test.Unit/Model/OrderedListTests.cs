using FluentAssertions;
using MarkdownDocumentBuilder.Model;
using MarkdownDocumentBuilder.Test.Unit.TestHelpers;
using MarkdownDocumentBuilder.Test.Unit.TestModel;

namespace MarkdownDocumentBuilder.Test.Unit.Model;

[TestClass]
public class OrderedListTests
{
    [TestMethod]
    public void ToMarkdown_SimpleList_ReturnsExpectedMarkdown()
    {
        // Arrange
        var simpleList = new string[] { "FirstItem", "SecondItem" };

        var document = MarkdownDocument.Build(document =>
        {
            document.Content(content =>
            {
                content.AddOrderedList(simpleList);
            });
        });

        using var stream = new MemoryStream();

        // Act
        document.SaveAsync(stream);

        // Assert
        string result = stream.ReadAsString();
        string filePath = Path.Combine("Resources/List", "ExpectedSimpleList.md");
        var expectedDocument = FileReader.ReadFile(filePath);
        result.Should().Be(expectedDocument);
    }

    [TestMethod]
    public void ToMarkdown_ComplexList_ReturnsExpectedMarkdown()
    {
        // Arrange
        var complexList = CreateComplexList();

        var document = MarkdownDocument.Build(document =>
        {
            document.Content(content =>
            {
                content.AddOrderedList(complexList);
            });
        });

        using var stream = new MemoryStream();

        // Act
        document.SaveAsync(stream);

        // Assert
        string result = stream.ReadAsString();
        string filePath = Path.Combine("Resources/List", "ExpectedComplexList.md");
        var expectedDocument = FileReader.ReadFile(filePath);
        result.Should().Be(expectedDocument);
    }

    [TestMethod]
    public void ToMarkdown_TwoComplexLists_ReturnsExpectedMarkdown()
    {
        // Arrange
        var complexList = CreateComplexList();

        var document = MarkdownDocument.Build(document =>
        {
            document.Content(content =>
            {
                content.AddOrderedList(complexList, complexList);
            });
        });

        using var stream = new MemoryStream();

        // Act
        document.SaveAsync(stream);

        // Assert
        string result = stream.ReadAsString();
        string filePath = Path.Combine("Resources/List", "ExpectedDoubleComplexList.md");
        var expectedDocument = FileReader.ReadFile(filePath);
        result.Should().Be(expectedDocument);
    }

    private static ComplexList CreateComplexList() => new()
    {
        FirstItem = "FirstItem",
        NestedObject = new()
        {
            FirstNestedItem = "FirstNestedItem",
            NestedList = new List<ListItem>
            {
                new ListItem()
                {
                    FirstNestedListItem = "FirstNestedListItem",
                    SecondNestedListItem = "SecondNestedListItem"
                }
            },
            ThirdNestedItem = "ThirdNestedItem",
        },
        ThirdItem = "ThirdItem"
    };
}
