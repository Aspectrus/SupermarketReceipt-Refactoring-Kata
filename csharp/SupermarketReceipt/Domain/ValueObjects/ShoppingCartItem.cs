using SupermarketReceipt.Domain.Entities;
using System;

namespace SupermarketReceipt.Domain.ValueObjects
{
    public class ShoppingCartItem
    {
        public Product Product { get; }
        public Quantity Quantity { get; }

        public ShoppingCartItem(Product product, Quantity quantity)
        {
            Product = product ?? throw new ArgumentNullException(nameof(product));
            Quantity = quantity ?? throw new ArgumentNullException(nameof(quantity));
            if (quantity.Unit != product.Unit)
                throw new ArgumentException("Quantity unit must match product unit.", nameof(quantity));
        }
    }
}
