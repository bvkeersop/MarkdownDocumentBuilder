﻿using MarkdownDocumentBuilder.Model.Elements.Table.Attributes;
using MarkdownDocumentBuilder.Model.Elements.Table.Options;

namespace MarkdownDocumentBuilder.Test.Unit.TestModel
{
    public class ProductTableRowWithColumnAttribute
    {
        [Column(name: "ProductId", alignment: Alignment.Left, order: 1)] // Overwrite name
        public string Id { get; set; }

        [Column(nameof(Description), alignment: Alignment.Right)] // No order will be last
        public string Description { get; set; }

        [Column(nameof(Price), alignment: Alignment.Center, order: 3)]
        public string Price { get; set; }

        [Column(nameof(Amount), alignment: Alignment.Center, order: 2)]
        public string Amount { get; set; }
    }
}