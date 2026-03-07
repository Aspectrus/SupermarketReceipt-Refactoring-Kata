using SupermarketReceipt.Domain.Entities;
using SupermarketReceipt.Domain.Interfaces;
using System.Collections.Generic;

namespace SupermarketReceipt.Infrastructure
{
    public class FakeCatalog : ICatalog
    {
        private readonly IDictionary<string, decimal> _prices = new Dictionary<string, decimal>();
        private readonly IDictionary<string, Product> _products = new Dictionary<string, Product>();

        public void AddProduct(Product product, decimal price)
        {
            _products.Add(product.Name, product);
            _prices.Add(product.Name, price);
        }

        public decimal GetUnitPrice(Product p)
        {
            return _prices[p.Name];
        }
    }
}