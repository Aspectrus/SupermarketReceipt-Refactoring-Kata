using SupermarketReceipt.Domain.Entities.Offers;
using SupermarketReceipt.Domain.Interfaces;
using SupermarketReceipt.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Collections.ObjectModel;

namespace SupermarketReceipt.Domain.Entities
{
    public class ShoppingCart
    {
        private readonly List<ProductQuantity> _items = new List<ProductQuantity>();
        private readonly Dictionary<Product, Quantity> _productQuantities = new Dictionary<Product, Quantity>();


        public List<ProductQuantity> GetItems()
        {
            return new List<ProductQuantity>(_items);
        }
        public IReadOnlyDictionary<Product, Quantity> GetProductQuantities()
        {
            return _productQuantities.AsReadOnly();
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
            _items.Add(new ProductQuantity(product, quantity));
            if (_productQuantities.ContainsKey(product))
            {
                var newAmount = _productQuantities[product].Add(quantity);
                _productQuantities[product] = newAmount;
            }
            else
            {
                _productQuantities.Add(product, quantity);
            }

        }
    }
}