using SupermarketReceipt.Domain.Interfaces;
using SupermarketReceipt.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace SupermarketReceipt.Domain.Entities.Offers
{
    internal class BuyXPayYOffer : Offer
    {
        public int ItemsReceived { get; }
        public int ItemsPaid { get; }

        public BuyXPayYOffer(int itemsReceived, int itemsPaid, Product product)
        : base(product, SpecialOfferType.BuyXPayYOffer)
        {
            if (itemsReceived <= 0)
                throw new ArgumentOutOfRangeException(nameof(itemsReceived), "Items received must be positive.");
            if (itemsPaid <= 0)
                throw new ArgumentOutOfRangeException(nameof(itemsPaid), "Items paid must be positive.");
            if (itemsPaid > itemsReceived)
                throw new ArgumentException("Items paid cannot exceed items received.", nameof(itemsPaid));
            if (product == null)
                throw new ArgumentNullException(nameof(product), "Product cannot be null.");

            ItemsReceived = itemsReceived;
            ItemsPaid = itemsPaid;
            Product = product;

        }

        private Discount CalculateDiscountForProduct(Product product, Quantity quantity, decimal unitPrice)
        {

            int totalUnits = (int)quantity.Amount;

            if (totalUnits < ItemsReceived)
                return null;

            int groups = totalUnits / ItemsReceived;
            int freeItemsPerGroup = ItemsReceived - ItemsPaid;
            int totalFreeItems = groups * freeItemsPerGroup;
            decimal discount = totalFreeItems * unitPrice;

            return discount > 0
               ? new Discount(
               product,
               discount,
               SpecialOfferType,
               new decimal[] { ItemsReceived, ItemsPaid })
               : null;
        }

        public override Discount CalculateDiscount(IReadOnlyDictionary<Product, Quantity> productQuantities, ICatalog catalog)
        {

            if (productQuantities.TryGetValue(Product, out var prodQuantity))
            {
                return CalculateDiscountForProduct(Product, prodQuantity, catalog.GetUnitPrice(Product));
            }

            return null;

        }
    }
}