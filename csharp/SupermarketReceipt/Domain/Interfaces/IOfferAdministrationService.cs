using SupermarketReceipt.Domain.Entities;
using SupermarketReceipt.Domain.Entities.Offers;
using System.Collections.ObjectModel;

namespace SupermarketReceipt.Domain.Interfaces
{
    public interface IOfferAdministrationService
    {
        void AddSpecialOffer(SpecialOfferType offerType, Product product, decimal argument);
        ReadOnlyDictionary<Product, Offer> GetAllOffers();
    }
}