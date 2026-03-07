using SupermarketReceipt.Domain.Interfaces;
using SupermarketReceipt.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace SupermarketReceipt.Domain.Entities.Offers
{
    public enum SpecialOfferType
    {
        ThreeForTwo,
        TenPercentDiscount,
        TwoForAmount,
        FiveForAmount
    }

    public abstract class Offer
    {
        public SpecialOfferType OfferType { get; internal set; }
        public decimal Argument { get; internal set; }

        public abstract Discount CalculateDiscountForProduct(Product product, Quantity quantity, decimal unitPrice);
    }
}