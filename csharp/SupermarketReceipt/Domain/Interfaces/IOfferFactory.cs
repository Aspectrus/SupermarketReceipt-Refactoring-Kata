using SupermarketReceipt.Domain.Entities;
using SupermarketReceipt.Domain.Entities.Offers;

namespace SupermarketReceipt.Domain.Services
{
    public interface IOfferFactory
    {
        Offer CreateFiveForAmountOffer(Product product, decimal amount);
        Offer CreateTenPercentDiscountOffer(Product product);
        Offer CreateThreeForTwoOffer(Product product);
        Offer CreateTwoForAmountOffer(Product product, decimal amount);
    }
}