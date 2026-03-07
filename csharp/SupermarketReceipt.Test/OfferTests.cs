using SupermarketReceipt.Domain.Entities;
using SupermarketReceipt.Domain.Interfaces;
using SupermarketReceipt.Domain.Services;
using SupermarketReceipt.Domain.ValueObjects;
using SupermarketReceipt.Infrastructure;
using System;
using System.Collections.Generic;
using Xunit;

namespace SupermarketReceipt.Test.Offers
{
    public class OfferTests
    {
        private readonly ICatalog _catalog;
        private readonly IOfferFactory _factory;
        private readonly Product _toothbrush;
        private readonly Product _apples;

        public OfferTests()
        {
            _catalog = new FakeCatalog();
            _toothbrush = new Product("toothbrush", ProductUnit.Each);
            _apples = new Product("apples", ProductUnit.Kilo);
            _catalog.AddProduct(_toothbrush, 0.99m);
            _catalog.AddProduct(_apples, 1.99m);
            _factory = new OfferFactory();
        }

        [Fact]
        public void CreateTwoForAmountOffer_NegativeAmount_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                _factory.CreateTwoForAmountOffer(_toothbrush, -1m));
        }

        [Fact]
        public void CreateTwoForAmountOffer_ZeroAmount_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                _factory.CreateTwoForAmountOffer(_toothbrush, 0m));
        }

        [Fact]
        public void CreateTwoForAmountOffer_NullProduct_Throws()
        {
            Assert.Throws<ArgumentNullException>(() =>
                _factory.CreateTwoForAmountOffer(null, 1.5m));
        }

        [Fact]
        public void TwoForAmount_QuantityLessThanGroup_ReturnsNull()
        {
            var offer = _factory.CreateTwoForAmountOffer(_toothbrush, 1.5m);
            var quantities = new Dictionary<Product, Quantity>
            {
                { _toothbrush, Quantity.ForProduct(_toothbrush, 1) }
            };
            var discount = offer.CalculateDiscount(quantities, _catalog);
            Assert.Null(discount);
        }

        [Fact]
        public void TwoForAmount_ExactlyOneGroup_ReturnsCorrectDiscount()
        {
            var offer = _factory.CreateTwoForAmountOffer(_toothbrush, 1.5m);
            var quantities = new Dictionary<Product, Quantity>
            {
                { _toothbrush, Quantity.ForProduct(_toothbrush, 2) }
            };
            var discount = offer.CalculateDiscount(quantities, _catalog);
            Assert.NotNull(discount);
            Assert.Equal(0.48m, discount.DiscountAmount);
        }

        [Fact]
        public void TwoForAmount_MultipleGroups_ReturnsCorrectDiscount()
        {
            var offer = _factory.CreateTwoForAmountOffer(_toothbrush, 1.5m);
            var quantities = new Dictionary<Product, Quantity>
            {
                { _toothbrush, Quantity.ForProduct(_toothbrush, 5) }
            };
            var discount = offer.CalculateDiscount(quantities, _catalog);
            Assert.NotNull(discount);
            Assert.Equal(0.96m, discount.DiscountAmount);
        }

        [Fact]
        public void TwoForAmount_WhenOfferAmountExceedsRegularPrice_ReturnsNull()
        {
            var offer = _factory.CreateTwoForAmountOffer(_toothbrush, 2.5m);
            var quantities = new Dictionary<Product, Quantity>
            {
                { _toothbrush, Quantity.ForProduct(_toothbrush, 2) }
            };
            var discount = offer.CalculateDiscount(quantities, _catalog);
            Assert.Null(discount);
        }

        [Fact]
        public void CreateThreeForTwoOffer_NullProduct_Throws()
        {
            Assert.Throws<ArgumentNullException>(() =>
                _factory.CreateThreeForTwoOffer(null));
        }

        [Fact]
        public void ThreeForTwo_QuantityLessThanRequired_ReturnsNull()
        {
            var offer = _factory.CreateThreeForTwoOffer(_toothbrush);
            var quantities = new Dictionary<Product, Quantity>
            {
                { _toothbrush, Quantity.ForProduct(_toothbrush, 2) }
            };
            var discount = offer.CalculateDiscount(quantities, _catalog);
            Assert.Null(discount);
        }

        [Fact]
        public void ThreeForTwo_ExactlyOneGroup_ReturnsCorrectDiscount()
        {
            var offer = _factory.CreateThreeForTwoOffer(_toothbrush);
            var quantities = new Dictionary<Product, Quantity>
            {
                { _toothbrush, Quantity.ForProduct(_toothbrush, 3) }
            };
            var discount = offer.CalculateDiscount(quantities, _catalog);
            Assert.NotNull(discount);
            Assert.Equal(0.99m, discount.DiscountAmount);
        }

        [Fact]
        public void ThreeForTwo_MultipleGroups_ReturnsCorrectDiscount()
        {
            var offer = _factory.CreateThreeForTwoOffer(_toothbrush);
            var quantities = new Dictionary<Product, Quantity>
            {
                { _toothbrush, Quantity.ForProduct(_toothbrush, 7) }
            };
            var discount = offer.CalculateDiscount(quantities, _catalog);
            Assert.NotNull(discount);
            Assert.Equal(1.98m, discount.DiscountAmount);
        }


        [Fact]
        public void CreateTenPercentDiscountOffer_NullProduct_Throws()
        {
            Assert.Throws<ArgumentNullException>(() =>
                _factory.CreateTenPercentDiscountOffer(null));
        }

        [Fact]
        public void TenPercent_WithUnitProduct_ReturnsCorrectDiscount()
        {
            var offer = _factory.CreateTenPercentDiscountOffer(_toothbrush);
            var quantities = new Dictionary<Product, Quantity>
            {
                { _toothbrush, Quantity.ForProduct(_toothbrush, 2) }
            };
            var discount = offer.CalculateDiscount(quantities, _catalog);
            Assert.NotNull(discount);
            Assert.Equal(0.198m, discount.DiscountAmount);
        }

        [Fact]
        public void TenPercent_FractionalQuantity_ReturnsCorrectDiscount()
        {
            var offer = _factory.CreateTenPercentDiscountOffer(_apples);
            var quantities = new Dictionary<Product, Quantity>
            {
                { _apples, Quantity.ForProduct(_apples, 2.5m) }
            };
            var discount = offer.CalculateDiscount(quantities, _catalog);
            Assert.NotNull(discount);
            Assert.Equal(0.4975m, discount.DiscountAmount);
        }
    }
}