using FluentAssertions;
using MarkdownDocumentBuilder.Model.Document;
using MarkdownDocumentBuilder.Test.Unit.TestHelpers;
using MarkdownDocumentBuilder.Test.Unit.TestModel;

namespace MarkdownDocumentBuilder.Test.Unit.Model.Elements.Lists;

[TestClass]
public class OrderedListTests
{
    [TestMethod]
    public void ToMarkdown_SimpleArray_ReturnsExpectedMarkdown()
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
    public void ToMarkdown_SimpleList_ReturnsExpectedMarkdown()
    {
        // Arrange
        var simpleList = new List<string> { "FirstItem", "SecondItem" };

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
    public void ToMarkdown_SimpleEnumerable_ReturnsExpectedMarkdown()
    {
        // Arrange
        var simpleList = Enumerable.Empty<string>();
        simpleList = simpleList.Append("FirstItem");
        simpleList = simpleList.Append("SecondItem");

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
