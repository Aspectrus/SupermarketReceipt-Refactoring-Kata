using SupermarketReceipt.Domain.Interfaces;
using SupermarketReceipt.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace SupermarketReceipt.Domain.Entities.Offers
{
    internal class BuyXForYAmountOffer : Offer
    {
        public decimal Amount { get; }
        public int GroupSize { get; }

        public BuyXForYAmountOffer(int groupSize, decimal amount, Product product, SpecialOfferType type)
        : base(product, type)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be positive.");
            if (groupSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(groupSize), "Group size must be positive.");
            if (product == null)
                throw new ArgumentNullException(nameof(product), "Product cannot be null.");

            Amount = amount;
            GroupSize = groupSize;
            Product = product;
        }

        public override Discount CalculateDiscountForProduct(Product product, Quantity quantity, decimal unitPrice)
        {

            int totalUnits = (int)quantity.Amount;

            if (totalUnits < GroupSize)
                return null;

            int groups = totalUnits / GroupSize;
            int remainder = totalUnits % GroupSize;

            decimal totalWithoutDiscount = unitPrice * totalUnits;
            decimal totalWithOffer = groups * Amount + remainder * unitPrice;
            decimal discount = totalWithoutDiscount - totalWithOffer;

            return discount > 0
                ? new Discount(
                    product,
                    string.Format("{0:0} for {1:N2}", GroupSize, Amount),
                    -discount)
                : null;
        }
    }
}