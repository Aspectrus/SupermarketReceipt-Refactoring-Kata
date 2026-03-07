using SupermarketReceipt.Domain.Interfaces;
using SupermarketReceipt.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace SupermarketReceipt.Domain.Entities.Offers
{
    public class TenPercentDiscountOffer : Offer
    {
        public TenPercentDiscountOffer(decimal percent) 
        {
            OfferType = SpecialOfferType.TenPercentDiscount;
            Argument = percent;
        }

        public override Discount CalculateDiscountForProduct(Product product, Quantity quantity, decimal unitPrice)
        {
            decimal discount = quantity.Amount * unitPrice * Argument / 100.0m;
            return discount > 0
                ? new Discount(product, $"{Argument:0.##}% off", -discount)
                : null;
        }
    }
}