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

        public void AddSpecialOffer(SpecialOfferType offerType, Product product, decimal argument)
        {
            Offer offer = offerType switch
            {
                SpecialOfferType.ThreeForTwo => new ThreeForTwoOffer(),
                SpecialOfferType.TwoForAmount => new TwoForAmountOffer(argument),
                SpecialOfferType.FiveForAmount => new FiveForAmountOffer(argument),
                SpecialOfferType.TenPercentDiscount => new TenPercentDiscountOffer(argument),
                _ => throw new ArgumentOutOfRangeException(nameof(offerType), offerType, null)
            };
            _offerRepository.Add(offer, product);
        }
    }
}
