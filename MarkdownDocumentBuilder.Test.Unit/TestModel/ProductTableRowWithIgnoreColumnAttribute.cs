using MarkdownDocumentBuilder.Model.Elements.Table.Attributes;

namespace MarkdownDocumentBuilder.Test.Unit.TestModel;

internal class ProductTableRowWithIgnoreColumnAttribute
{
    public string Id { get; set; }

    [IgnoreColumn]
    public string Amount { get; set; }

    public string Price { get; set; }

    [IgnoreColumn]
    public string Description { get; set; }
}
