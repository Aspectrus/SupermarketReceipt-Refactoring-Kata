using SupermarketReceipt.Domain.Entities;
using SupermarketReceipt.Domain.Entities.Offers;
using SupermarketReceipt.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SupermarketReceipt.Infrastructure
{
    public class InMemoryOfferRepository : IOfferRepository
    {
        private readonly Dictionary<Product, Offer> _offers = new();

        public void Add(Offer offer)
        {
            if (offer == null)
                throw new ArgumentNullException(nameof(offer));
            _offers[offer.Product] = offer;
        }

        public ReadOnlyDictionary<Product, Offer> GetAll()
        {
            return _offers.AsReadOnly();
        }
    }
}
