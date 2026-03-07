using SupermarketReceipt.Domain.Entities;
using SupermarketReceipt.Domain.Entities.Offers;
using SupermarketReceipt.Domain.Interfaces;
using System;
using System.Collections.ObjectModel;

namespace SupermarketReceipt.Application
{
    public class OfferAdministrationService : IOfferAdministrationService
    {
        private readonly IOfferRepository _offerRepository;

        public OfferAdministrationService(IOfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }

        public ReadOnlyDictionary<Product, Offer> GetAllOffers() => _offerRepository.GetAll();

        public void AddOffer(Offer offer) => _offerRepository.Add(offer);

    }
}
