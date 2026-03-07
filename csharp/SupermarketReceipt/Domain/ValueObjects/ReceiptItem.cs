using SupermarketReceipt.Domain.Entities;
using System;

namespace SupermarketReceipt.Domain.ValueObjects
{
    public class ReceiptItem
    {
        public Product Product { get; }
        public Quantity Quantity { get; }
        public decimal Price { get; }
        public decimal TotalPrice { get; }

        public ReceiptItem(Product product, Quantity quantity, decimal price, decimal totalPrice)
        {
            Product = product ?? throw new ArgumentNullException(nameof(product));
            Quantity = quantity ?? throw new ArgumentNullException(nameof(quantity));
            if (price < 0) throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative.");
            if (totalPrice != price * quantity.Amount)
                throw new ArgumentException("Total price must equal price × quantity.");
            Price = price;
            TotalPrice = totalPrice;
        }
    }
}
