using SupermarketReceipt.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace SupermarketReceipt.Domain.Entities
{
    public class ShoppingCart
    {
        private readonly List<ShoppingCartItem> _items = new List<ShoppingCartItem>();

        public IReadOnlyDictionary<Product, Quantity> GetProductQuantities()
        {
            var result = new Dictionary<Product, Quantity>();
            foreach (var item in _items)
            {
                if (result.TryGetValue(item.Product, out var existing))
                    result[item.Product] = existing.Add(item.Quantity);
                else
                    result[item.Product] = item.Quantity;
            }
            return result;
        }


        public List<ShoppingCartItem> GetItems()
        {
            return new List<ShoppingCartItem>(_items);
        }

        public void AddItem(Product product)
        {
            var defaultQuantity = Quantity.ForProduct(product);
            AddItemQuantity(product, defaultQuantity);
        }


        public void AddItemQuantity(Product product, Quantity quantity)
        {
            if (product.Unit != quantity.Unit)
                throw new ArgumentException($"Product unit {product.Unit} doesn't match quantity unit {quantity.Unit}");

            _items.Add(new ShoppingCartItem(product, quantity));
        }

    }
}