using SupermarketReceipt.Domain.Interfaces;
using SupermarketReceipt.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace SupermarketReceipt.Domain.Entities.Offers
{
    public class TwoForAmountOffer : Offer
    {
        public TwoForAmountOffer(decimal amount) 
        {
            OfferType = SpecialOfferType.TwoForAmount;
            Argument = amount;
        }

        public override Discount CalculateDiscountForProduct(Product product, Quantity quantity, decimal unitPrice)
        {
            int totalUnits = (int)quantity.Amount;
            if (totalUnits < 2)
                return null;

            int groups = totalUnits / 2;
            int remainder = totalUnits % 2;

            decimal totalWithoutDiscount = unitPrice * totalUnits;
            decimal totalWithOffer = groups * Argument + remainder * unitPrice;
            decimal discount = totalWithoutDiscount - totalWithOffer;

            return discount > 0
                ? new Discount(product, $"2 for {Argument:N2}", -discount)
                : null;
        }
    }
}