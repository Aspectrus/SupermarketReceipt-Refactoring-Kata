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
        private readonly Dictionary<Product, Offer> _offers = new Dictionary<Product, Offer>();

        public Teller(ICatalog catalog)
        {
            _catalog = catalog;
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

        public Receipt ChecksOutArticlesFrom(ShoppingCart theCart)
        {
            var receipt = new Receipt();
            var productQuantities = theCart.GetItems();
            foreach (var pq in productQuantities)
            {
                var p = pq.Product;
                var quantity = pq.Quantity.Amount;
                var unitPrice = _catalog.GetUnitPrice(p);
                var price = quantity * unitPrice;
                receipt.AddProduct(p, quantity, unitPrice, price);
            }

            theCart.HandleOffers(receipt, _offers, _catalog);

            return receipt;
        }
    }
}