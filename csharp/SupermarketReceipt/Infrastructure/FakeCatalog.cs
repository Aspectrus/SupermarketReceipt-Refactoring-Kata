using SupermarketReceipt.Domain.Entities;
using SupermarketReceipt.Domain.Interfaces;
using SupermarketReceipt.Domain.ValueObjects;
using System.Collections.Generic;

namespace SupermarketReceipt.Infrastructure
{
    public class FakeCatalog : ICatalog
    {
        private readonly IDictionary<Product, CatalogEntry> _catalogueEntries = new Dictionary<Product, CatalogEntry>();

        public void AddProduct(Product product, decimal price)
        {
            _catalogueEntries[product] = new CatalogEntry(product, price);
        }

        public decimal GetUnitPrice(Product product)
        {
            return _catalogueEntries[product].Price;
        }

    }
}