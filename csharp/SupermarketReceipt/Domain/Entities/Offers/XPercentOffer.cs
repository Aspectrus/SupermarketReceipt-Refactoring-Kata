using SupermarketReceipt.Domain.Interfaces;
using SupermarketReceipt.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace SupermarketReceipt.Domain.Entities.Offers
{
    internal class XPercentOffer : Offer
    {
        public XPercentOffer(decimal percentAmount, Product product, SpecialOfferType type)
        : base(product, type)
        {
            if (percentAmount <= 0 || percentAmount > 100)
                throw new ArgumentOutOfRangeException(nameof(percentAmount), "Percent amount must be between 0 and 100 (exclusive for 0).");
            if (product == null)
                throw new ArgumentNullException(nameof(product), "Product cannot be null.");

            PercentAmount = percentAmount;
            Product = product;

        }
        public decimal PercentAmount { get; }

        public override Discount CalculateDiscountForProduct(Product product, Quantity quantity, decimal unitPrice)
        {
            return new Discount(product, string.Format("{0:0.##}% off", PercentAmount), -quantity.Amount * unitPrice * PercentAmount / 100.0M);
        }
    }
}
