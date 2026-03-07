using SupermarketReceipt.Domain.Entities;

namespace SupermarketReceipt.Domain.Interfaces
{
    public interface ISupermarketCatalog
    {
        void AddProduct(Product product, decimal price);

        decimal GetUnitPrice(Product product);
    }
}