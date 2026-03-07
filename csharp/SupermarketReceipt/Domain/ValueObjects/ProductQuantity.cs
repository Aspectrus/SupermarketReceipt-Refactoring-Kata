using SupermarketReceipt.Domain.Entities;

namespace SupermarketReceipt.Domain.ValueObjects
{
    public class ProductQuantity
    {
        public ProductQuantity(Product product, Quantity quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public Product Product { get; }
        public Quantity Quantity { get; }
    }
}