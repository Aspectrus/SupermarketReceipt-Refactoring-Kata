using SupermarketReceipt.Domain.Entities.Offers;
using SupermarketReceipt.Domain.Interfaces;
using SupermarketReceipt.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace SupermarketReceipt.Domain.Entities
{
    public class ShoppingCart
    {
        private readonly List<ProductQuantity> _items = new List<ProductQuantity>();
        private readonly Dictionary<Product, Quantity> _productQuantities = new Dictionary<Product, Quantity>();
        private static readonly CultureInfo Culture = CultureInfo.CreateSpecificCulture("en-GB");


        public List<ProductQuantity> GetItems()
        {
            return new List<ProductQuantity>(_items);
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

        public void HandleOffers(Receipt receipt, Dictionary<Product, Offer> offers, ICatalog catalog)
        {
            foreach (var p in _productQuantities.Keys)
            {
                var quantity = _productQuantities[p];
                var quantityAsInt = (int) quantity.Amount;
                if (offers.ContainsKey(p))
                {
                    var offer = offers[p];
                    var unitPrice = catalog.GetUnitPrice(p);
                    Discount discount = null;
                    var x = 1;
                    if (offer.OfferType == SpecialOfferType.ThreeForTwo)
                    {
                        x = 3;
                    }
                    else if (offer.OfferType == SpecialOfferType.TwoForAmount)
                    {
                        x = 2;
                        if (quantityAsInt >= 2)
                        {
                            var total = offer.Argument * (quantityAsInt / x) + quantityAsInt % 2 * unitPrice;
                            var discountN = unitPrice * quantity.Amount - total;
                            discount = new Discount(p, "2 for " + PrintPrice(offer.Argument), -discountN);
                        }
                    }

                    if (offer.OfferType == SpecialOfferType.FiveForAmount) x = 5;
                    var numberOfXs = quantityAsInt / x;
                    if (offer.OfferType == SpecialOfferType.ThreeForTwo && quantityAsInt > 2)
                    {
                        var discountAmount = quantity.Amount * unitPrice - (numberOfXs * 2 * unitPrice + quantityAsInt % 3 * unitPrice);
                        discount = new Discount(p, "3 for 2", -discountAmount);
                    }

                    if (offer.OfferType == SpecialOfferType.TenPercentDiscount) discount = new Discount(p, offer.Argument.ToString("0.##") + "% off", -quantity.Amount * unitPrice * offer.Argument / 100.0M);
                    if (offer.OfferType == SpecialOfferType.FiveForAmount && quantityAsInt >= 5)
                    {
                        var discountTotal = unitPrice * quantity.Amount - (offer.Argument * numberOfXs + quantityAsInt % 5 * unitPrice);
                        discount = new Discount(p, x + " for " + PrintPrice(offer.Argument), -discountTotal);
                    }

                    if (discount != null)
                        receipt.AddDiscount(discount);
                }
            }
        }
        
        private string PrintPrice(decimal price)
        {
            return price.ToString("N2", Culture);
        }
    }
}