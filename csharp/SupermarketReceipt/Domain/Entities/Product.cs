using SupermarketReceipt.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace SupermarketReceipt.Domain.Entities
{
    public class Product
    {
        public Product(string name, ProductUnit unit)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name cannot be null or whitespace.", nameof(name));

            if (!Enum.IsDefined(typeof(ProductUnit), unit))
                throw new ArgumentOutOfRangeException(nameof(unit), "Invalid product unit.");

            Name = name;
            Unit = unit;
        }

        public string Name { get; }
        public ProductUnit Unit { get; }

        public override bool Equals(object obj)
        {
            var product = obj as Product;
            return product != null &&
                   Name == product.Name &&
                   Unit == product.Unit;
        }

        public override int GetHashCode()
        {
            var hashCode = -1996304355;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + Unit.GetHashCode();
            return hashCode;
        }
    }
}