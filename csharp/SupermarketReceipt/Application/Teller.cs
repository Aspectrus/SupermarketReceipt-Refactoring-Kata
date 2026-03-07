using SupermarketReceipt.Domain.Entities;
using SupermarketReceipt.Domain.Entities.Offers;
using SupermarketReceipt.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace SupermarketReceipt.Application
{
    public class Teller
    {
        private readonly ICatalog _catalog; 
        private readonly IDiscountCalculator _discountCalculator;

        private readonly Dictionary<Product, Offer> _offers = new Dictionary<Product, Offer>();

        public Teller(ICatalog catalog, IDiscountCalculator discountCalculator)
        {
            _catalog = catalog;
            _discountCalculator = discountCalculator;
        }

        public void AddSpecialOffer(SpecialOfferType offerType, Product product, decimal argument)
        {
            Offer offer = offerType switch
            {
                SpecialOfferType.ThreeForTwo => new ThreeForTwoOffer(),
                SpecialOfferType.TwoForAmount => new TwoForAmountOffer(argument),
                SpecialOfferType.FiveForAmount => new FiveForAmountOffer(argument),
                SpecialOfferType.TenPercentDiscount => new TenPercentDiscountOffer(argument),
                _ => throw new ArgumentOutOfRangeException(nameof(offerType), offerType, null)
            };
            _offers[product] = offer;
        }

        public Receipt ChecksOutArticlesFrom(ShoppingCart cart)
        {
            var receipt = new Receipt();
            AddItemsToReceipt(receipt, cart);
            ApplyDiscountsToReceipt(receipt, cart);
            return receipt;
        }

        private void AddItemsToReceipt(Receipt receipt, ShoppingCart cart)
        {
            var productQuantities = cart.GetItems();

            foreach (var pq in productQuantities)
            {
                var p = pq.Product;
                var quantity = pq.Quantity;
                var unitPrice = _catalog.GetUnitPrice(p);
                receipt.AddProduct(p, quantity.Amount, unitPrice, quantity.Amount * unitPrice);
            }
        }

        private void ApplyDiscountsToReceipt(Receipt receipt, ShoppingCart cart)
        {
            var discounts = _discountCalculator.GetDiscountsFromOffers(_offers, cart.GetProductQuantities());
            discounts.ForEach(receipt.AddDiscount);
        }
    }
}