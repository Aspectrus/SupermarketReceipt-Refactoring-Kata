using SupermarketReceipt.Domain.Entities;
using SupermarketReceipt.Domain.Entities.Offers;

namespace SupermarketReceipt.Domain.Services
{
    public class OfferFactory : IOfferFactory
    {
        public Offer CreateThreeForTwoOffer(Product product)
        {
            return new BuyXPayYOffer(3, 2, product);
        }

        public Offer CreateTenPercentDiscountOffer(Product product)
        {
            return new XPercentOffer(10, product);
        }

        public Offer CreateTwoForAmountOffer(Product product, decimal amount)
        {
            return new BuyXForYAmountOffer(2, amount, product);
        }

        public Offer CreateFiveForAmountOffer(Product product, decimal amount)
        {
            return new BuyXForYAmountOffer(5, amount, product);
        }
    }
}
