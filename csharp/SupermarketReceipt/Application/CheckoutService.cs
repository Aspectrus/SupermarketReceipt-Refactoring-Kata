using SupermarketReceipt.Domain.Entities;
using SupermarketReceipt.Domain.Entities.Offers;
using SupermarketReceipt.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace SupermarketReceipt.Application
{
    public class CheckoutService : ICheckoutService
    {
        private readonly ICatalog _catalog;
        private readonly IDiscountCalculator _discountCalculator;
        private readonly IOfferAdministrationService _offerAdministrationService;

        public CheckoutService(
             ICatalog catalog,
             IDiscountCalculator discountCalculator,
             IOfferAdministrationService offerAdministrationService)
        {
            _catalog = catalog;
            _discountCalculator = discountCalculator;
            _offerAdministrationService = offerAdministrationService;
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

            foreach (var productQuantity in productQuantities)
            {
                var product = productQuantity.Product;
                var quantity = productQuantity.Quantity;
                var unitPrice = _catalog.GetUnitPrice(product);
                receipt.AddProduct(product, quantity, unitPrice);
            }
        }

        private void ApplyDiscountsToReceipt(Receipt receipt, ShoppingCart cart)
        {
            var offers = _offerAdministrationService.GetAllOffers();
            var discounts = _discountCalculator.GetDiscountsFromOffers(offers, cart.GetProductQuantities());
            discounts.ForEach(receipt.AddDiscount);
        }
    }
}