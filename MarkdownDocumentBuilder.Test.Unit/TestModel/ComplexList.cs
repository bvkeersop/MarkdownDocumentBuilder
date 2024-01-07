using MarkdownDocumentBuilder.Abstractions;

namespace MarkdownDocumentBuilder.Test.Unit.TestModel;

internal class ComplexList : IListRepresentation
{
    public string FirstItem { get; set; }
    public NestedObject NestedObject { get; set; }
    public string ThirdItem { get; set; }
}

internal class NestedObject
{
    public string FirstNestedItem { get; set; }
    public IEnumerable<ListItem> NestedList { get; set; }
    public string ThirdNestedItem { get; set; }
}

internal class ListItem
{
    public string FirstNestedListItem { get; set; }
    public string SecondNestedListItem { get; set; }
}