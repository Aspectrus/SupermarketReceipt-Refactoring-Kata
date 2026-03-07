using SupermarketReceipt.Domain.Entities;

namespace SupermarketReceipt.Domain.ValueObjects
{
    public class ReceiptItem
    {
        public ReceiptItem(Product p, Quantity quantity, decimal price, decimal totalPrice)
        {
            Product = p;
            Quantity = quantity;
            Price = price;
            TotalPrice = totalPrice;
        }

        public Product Product { get; }
        public decimal Price { get; }
        public decimal TotalPrice { get; }
        public Quantity Quantity { get; }
    }
}