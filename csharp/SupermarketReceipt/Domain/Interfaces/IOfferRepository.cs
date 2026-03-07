using SupermarketReceipt.Domain.Entities;
using SupermarketReceipt.Domain.Entities.Offers;
using System.Collections.ObjectModel;

namespace SupermarketReceipt.Domain.Interfaces
{
    public interface IOfferRepository
    {
        void Add(Offer offer);
        ReadOnlyDictionary<Product, Offer> GetAll();
    }
}
