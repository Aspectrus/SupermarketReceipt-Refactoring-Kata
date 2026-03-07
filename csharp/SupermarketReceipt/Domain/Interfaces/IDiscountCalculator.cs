using SupermarketReceipt.Domain.Entities;
using SupermarketReceipt.Domain.Entities.Offers;
using SupermarketReceipt.Domain.ValueObjects;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SupermarketReceipt.Domain.Interfaces
{
    public interface IDiscountCalculator
    {
        List<Discount> GetDiscountsFromOffers(Dictionary<Product, Offer> offers, IReadOnlyDictionary<Product, Quantity> productQuantities);
    }
}