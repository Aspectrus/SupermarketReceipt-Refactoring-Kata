using SupermarketReceipt.Domain.Entities;
using System;

namespace SupermarketReceipt.Domain.ValueObjects
{
    public class CatalogEntry
    {
        public Product Product { get; }
        public decimal Price { get; }

        public CatalogEntry(Product product, decimal price)
        {
            Product = product ?? throw new ArgumentNullException(nameof(product));
            if (price <= 0)
                throw new ArgumentOutOfRangeException(nameof(price), "Price must be positive.");
            Price = price;
        }
    }
}
