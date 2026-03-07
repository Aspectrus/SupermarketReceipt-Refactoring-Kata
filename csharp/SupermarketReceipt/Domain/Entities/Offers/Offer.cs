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
        public Product Product { get; internal set; }
        public SpecialOfferType SpecialOfferType { get; internal set; }
        protected Offer(Product product, SpecialOfferType specialOfferType)
        {
            Product = product ?? throw new ArgumentNullException(nameof(product));
            SpecialOfferType = specialOfferType;
        }

        public abstract Discount CalculateDiscount(IReadOnlyDictionary<Product, Quantity> productQuantities, ICatalog catalog);
    }
}