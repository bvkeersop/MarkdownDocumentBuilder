using FluentAssertions;
using MarkdownDocumentBuilder.Model.Elements.Table;
using MarkdownDocumentBuilder.Test.Unit.TestModel;

namespace MarkdownDocumentBuilder.Test.Unit.Model.Elements.Table;

[TestClass]
public class TableReflectionHelperTests
{
    [TestMethod]
    public void GetOrderedTableRowPropertyInfos_NoHeaders_ReturnedInOrderOfProperties()
    {
        // Act
        var orderedTableRowPropertyInfos = TableReflectionHelper.GetOrderedTableRowPropertyInfos<ProductTableRowWithColumnAttribute>();

        // Assert
        orderedTableRowPropertyInfos.Count().Should().Be(4);
        orderedTableRowPropertyInfos.ElementAt(0).Name.Should().Be("Id");
        orderedTableRowPropertyInfos.ElementAt(1).Name.Should().Be("Amount");
        orderedTableRowPropertyInfos.ElementAt(2).Name.Should().Be("Price");
        orderedTableRowPropertyInfos.ElementAt(3).Name.Should().Be("Description");
    }

    [TestMethod]
    public void GetOrderedTableRowPropertyInfos_Headers_ReturnedInOrderOfCountPropertyOnHeader()
    {
        // Act
        var orderedTableRowPropertyInfos = TableReflectionHelper.GetOrderedTableRowPropertyInfos<ProductTableRowWithoutAttributes>();

        // Assert
        orderedTableRowPropertyInfos.Count().Should().Be(4);
        orderedTableRowPropertyInfos.ElementAt(0).Name.Should().Be("Id");
        orderedTableRowPropertyInfos.ElementAt(1).Name.Should().Be("Amount");
        orderedTableRowPropertyInfos.ElementAt(2).Name.Should().Be("Price");
        orderedTableRowPropertyInfos.ElementAt(3).Name.Should().Be("Description");
    }

    [TestMethod]
    public void GetOrderedColumnNames_NoHeaders_ReturnedInOrderOfProperties()
    {
        // Act
        var orderedTableRowPropertyInfos = TableReflectionHelper.GetOrderedTableRowPropertyInfos<ProductTableRowWithColumnAttribute>();

        // Assert
        orderedTableRowPropertyInfos.Count().Should().Be(4);
        orderedTableRowPropertyInfos.ElementAt(0).Name.Should().Be("Id");
        orderedTableRowPropertyInfos.ElementAt(1).Name.Should().Be("Amount");
        orderedTableRowPropertyInfos.ElementAt(2).Name.Should().Be("Price");
        orderedTableRowPropertyInfos.ElementAt(3).Name.Should().Be("Description");
    }

    [TestMethod]
    public void GetOrderedColumnNames_Headers_ReturnedInOrderOfCountPropertyOnHeader()
    {
        // Act
        var orderedTableRowPropertyInfos = TableReflectionHelper.GetOrderedTableRowPropertyInfos<ProductTableRowWithoutAttributes>();

        // Assert
        orderedTableRowPropertyInfos.Count().Should().Be(4);
        orderedTableRowPropertyInfos.ElementAt(0).Name.Should().Be("Id");
        orderedTableRowPropertyInfos.ElementAt(1).Name.Should().Be("Amount");
        orderedTableRowPropertyInfos.ElementAt(2).Name.Should().Be("Price");
        orderedTableRowPropertyInfos.ElementAt(3).Name.Should().Be("Description");
    }

    [TestMethod]
    public void GetOrderedTableRowPropertyInfos_IgnoreColumnAttributesPresent_DoesntIncludeIgnoredProperties()
    {
        // Act
        var orderedTableRowPropertyInfos = TableReflectionHelper.GetOrderedTableRowPropertyInfos<ProductTableRowWithIgnoreColumnAttribute>();

        // Assert
        orderedTableRowPropertyInfos.Count().Should().Be(2);
        orderedTableRowPropertyInfos.ElementAt(0).Name.Should().Be("Id");
        orderedTableRowPropertyInfos.ElementAt(1).Name.Should().Be("Price");
    }

    [TestMethod]
    public void GetOrderedTableRowPropertyInfos_EmptyTabelRow_ShouldStillReturnColumnNames()
    {
        // Act
        var orderedTableRowPropertyInfos = TableReflectionHelper.GetOrderedTableRowPropertyInfos<ProductTableRowWithoutAttributes>();

        // Assert
        orderedTableRowPropertyInfos.Count().Should().Be(4);
        orderedTableRowPropertyInfos.ElementAt(0).Name.Should().Be("Id");
        orderedTableRowPropertyInfos.ElementAt(1).Name.Should().Be("Amount");
        orderedTableRowPropertyInfos.ElementAt(2).Name.Should().Be("Price");
        orderedTableRowPropertyInfos.ElementAt(3).Name.Should().Be("Description");
    }
}
