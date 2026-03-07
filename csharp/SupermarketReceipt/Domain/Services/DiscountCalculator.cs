using SupermarketReceipt.Domain.Entities;
using SupermarketReceipt.Domain.Entities.Offers;
using SupermarketReceipt.Domain.Interfaces;
using SupermarketReceipt.Domain.ValueObjects;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SupermarketReceipt.Domain.Services
{

    public class DiscountCalculator : IDiscountCalculator
    {
        private readonly ICatalog _catalog;

        public DiscountCalculator(ICatalog catalog)
        {
            _catalog = catalog;
        }


        public List<Discount> GetDiscountsFromOffers(ReadOnlyDictionary<Product, Offer> offers, IReadOnlyDictionary<Product, Quantity> productQuantities)
        {

            var discounts = new List<Discount>();
            foreach (var (product, offer) in offers)
            {
                var discount = offer.CalculateDiscount(productQuantities, _catalog);

                if (discount != null)
                    discounts.Add(discount);
            }
            return discounts;
        }
    }
}
