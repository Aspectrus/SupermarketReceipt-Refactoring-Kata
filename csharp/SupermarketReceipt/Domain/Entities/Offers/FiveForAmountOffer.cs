using SupermarketReceipt.Domain.Interfaces;
using SupermarketReceipt.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace SupermarketReceipt.Domain.Entities.Offers
{
    public class FiveForAmountOffer : Offer
    {
        public FiveForAmountOffer(decimal amount) 
        {
            OfferType = SpecialOfferType.FiveForAmount;
            Argument = amount;
        }

        public override Discount CalculateDiscountForProduct(Product product, Quantity quantity, decimal unitPrice)
        {
            int totalUnits = (int)quantity.Amount;
            if (totalUnits < 5)
                return null;

            int groups = totalUnits / 5;
            int remainder = totalUnits % 5;

            decimal totalWithoutDiscount = unitPrice * totalUnits;
            decimal totalWithOffer = groups * Argument + remainder * unitPrice;
            decimal discount = totalWithoutDiscount - totalWithOffer;

            return discount > 0
                ? new Discount(product, $"5 for {Argument:N2}", -discount)
                : null;
        }
    }
}