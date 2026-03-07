using SupermarketReceipt.Domain.Entities;
using SupermarketReceipt.Domain.Entities.Offers;
using System.Collections.ObjectModel;

namespace SupermarketReceipt.Domain.Interfaces
{
    public interface IOfferAdministrationService
    {
        ReadOnlyDictionary<Product, Offer> GetAllOffers();
        void AddOffer(Offer offer);
    }
}
