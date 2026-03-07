using SupermarketReceipt.Domain.Entities;

namespace SupermarketReceipt.Domain.Interfaces
{
    public interface ICatalog
    {
        void AddProduct(Product product, decimal price);

        decimal GetUnitPrice(Product product);
    }
}