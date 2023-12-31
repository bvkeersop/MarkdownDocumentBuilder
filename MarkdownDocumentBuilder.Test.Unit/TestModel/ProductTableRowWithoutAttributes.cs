﻿namespace MarkdownDocumentBuilder.Test.Unit.TestModel
{
    public class ProductTableRowWithoutAttributes
    {
        public string Id { get; set; }
        public string Amount { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return Id + "-" + Amount + "-" + Price;
        }
    }
}
