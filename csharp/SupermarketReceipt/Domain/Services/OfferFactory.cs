using SupermarketReceipt.Domain.Entities;
using SupermarketReceipt.Domain.Entities.Offers;

namespace SupermarketReceipt.Domain.Services
{
    public class OfferFactory : IOfferFactory
    {
        public Offer CreateThreeForTwoOffer(Product product)
        {
            return new BuyXPayYOffer(3, 2, product, SpecialOfferType.ThreeForTwo);
        }

        public Offer CreateTenPercentDiscountOffer(Product product)
        {
            return new XPercentOffer(10, product, SpecialOfferType.TenPercentDiscount);
        }

        public Offer CreateTwoForAmountOffer(Product product, decimal amount)
        {
            return new BuyXForYAmountOffer(2, amount, product, SpecialOfferType.TwoForAmount);
        }

        public Offer CreateFiveForAmountOffer(Product product, decimal amount)
        {
            return new BuyXForYAmountOffer(5, amount, product, SpecialOfferType.FiveForAmount);
        }
    }
}
