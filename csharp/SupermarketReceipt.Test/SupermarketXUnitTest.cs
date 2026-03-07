using SupermarketReceipt.Application;
using SupermarketReceipt.Domain.Entities;
using SupermarketReceipt.Domain.Entities.Offers;
using SupermarketReceipt.Domain.Interfaces;
using SupermarketReceipt.Domain.Services;
using SupermarketReceipt.Domain.ValueObjects;
using SupermarketReceipt.Infrastructure;
using System.Threading.Tasks;
using VerifyXunit;
using Xunit;

namespace SupermarketReceipt.Test
{
    public class SupermarketXUnitTest
    {
        private ICatalog _catalog;
        private OfferAdministrationService _offerService;
        private ICheckoutService _checkoutService;
        private ShoppingCart _theCart;
        private OfferFactory _offerFactory;
        private Product _toothbrush;
        private Product _rice;
        private Product _apples;
        private Product _cherryTomatoes;
        private Product _candy;

        public SupermarketXUnitTest()
        {

            _catalog = new FakeCatalog();

            var discountCalculator = new DiscountCalculator(_catalog);
            var offerRepository = new InMemoryOfferRepository();

            _offerService = new OfferAdministrationService(offerRepository);
            _checkoutService = new CheckoutService(_catalog, discountCalculator, _offerService);
            _theCart = new ShoppingCart();
            _offerFactory = new OfferFactory();

            _toothbrush = new Product("toothbrush", ProductUnit.Each);
            _catalog.AddProduct(_toothbrush, 0.99m);
            _rice = new Product("rice", ProductUnit.Each);
            _catalog.AddProduct(_rice, 2.99m);
            _apples = new Product("apples", ProductUnit.Kilo);
            _catalog.AddProduct(_apples, 1.99m);
            _cherryTomatoes = new Product("cherry tomato box", ProductUnit.Each);
            _catalog.AddProduct(_cherryTomatoes, 0.69m);

            _candy = new Product("candy", ProductUnit.Kilo);
            _catalog.AddProduct(_candy, 0.07M);
        }


        [Fact]
        public Task precision_rounding_check()
        {
            _theCart.AddItemQuantity(_candy, Quantity.ForProduct(_candy, 0.5M));
            Receipt receipt = _checkoutService.ChecksOutArticlesFrom(_theCart);
            return Verifier.Verify(new ReceiptPrinter(40).PrintReceipt(receipt));
        }

        [Fact]
        public Task multiple_eoffers_for_one_product()
        {
            _theCart.AddItemQuantity(_apples, Quantity.ForProduct(_apples, 5M));
            var offer1 = _offerFactory.CreateFiveForAmountOffer(_apples, 5M);
            var offer2 = _offerFactory.CreateTenPercentDiscountOffer(_apples);
            var offer3 = _offerFactory.CreateTwoForAmountOffer(_apples, 3M);
            _offerService.AddOffer(offer1);
            _offerService.AddOffer(offer2);
            _offerService.AddOffer(offer3);

            Receipt receipt = _checkoutService.ChecksOutArticlesFrom(_theCart);

            return Verifier.Verify(new ReceiptPrinter(40).PrintReceipt(receipt));
        }

        [Fact]
        public Task an_empty_shopping_cart_should_cost_nothing()
        {
            Receipt receipt = _checkoutService.ChecksOutArticlesFrom(_theCart);
            return Verifier.Verify(new ReceiptPrinter(40).PrintReceipt(receipt));
        }

        [Fact]
        public Task one_normal_item()
        {
            _theCart.AddItem(_toothbrush);
            Receipt receipt = _checkoutService.ChecksOutArticlesFrom(_theCart);
            return Verifier.Verify(new ReceiptPrinter(40).PrintReceipt(receipt));
        }

        [Fact]
        public Task two_normal_items()
        {
            _theCart.AddItem(_toothbrush);
            _theCart.AddItem(_rice);
            Receipt receipt = _checkoutService.ChecksOutArticlesFrom(_theCart);
            return Verifier.Verify(new ReceiptPrinter(40).PrintReceipt(receipt));
        }

        [Fact]
        public Task buy_two_get_one_free()
        {
            _theCart.AddItem(_toothbrush);
            _theCart.AddItem(_toothbrush);
            _theCart.AddItem(_toothbrush);
            var offer = _offerFactory.CreateThreeForTwoOffer(_toothbrush);
            _offerService.AddOffer(offer);
            Receipt receipt = _checkoutService.ChecksOutArticlesFrom(_theCart);
            return Verifier.Verify(new ReceiptPrinter(40).PrintReceipt(receipt));
        }

        [Fact]
        public Task buy_five_get_one_free()
        {
            _theCart.AddItem(_toothbrush);
            _theCart.AddItem(_toothbrush);
            _theCart.AddItem(_toothbrush);
            _theCart.AddItem(_toothbrush);
            _theCart.AddItem(_toothbrush);
            var offer = _offerFactory.CreateThreeForTwoOffer(_toothbrush);
            _offerService.AddOffer(offer);
            Receipt receipt = _checkoutService.ChecksOutArticlesFrom(_theCart);
            return Verifier.Verify(new ReceiptPrinter(40).PrintReceipt(receipt));
        }

        [Fact]
        public Task loose_weight_product()
        {
            _theCart.AddItemQuantity(_apples, Quantity.ForProduct(_apples, 0.5M));
            Receipt receipt = _checkoutService.ChecksOutArticlesFrom(_theCart);
            return Verifier.Verify(new ReceiptPrinter(40).PrintReceipt(receipt));
        }

        [Fact]
        public Task percent_discount()
        {
            _theCart.AddItem(_rice);
            var offer = _offerFactory.CreateTenPercentDiscountOffer(_rice);
            _offerService.AddOffer(offer);
            Receipt receipt = _checkoutService.ChecksOutArticlesFrom(_theCart);
            return Verifier.Verify(new ReceiptPrinter(40).PrintReceipt(receipt));
        }

        [Fact]
        public Task xForY_discount()
        {
            _theCart.AddItem(_cherryTomatoes);
            _theCart.AddItem(_cherryTomatoes);
            var offer = _offerFactory.CreateTwoForAmountOffer(_cherryTomatoes, 0.99M);
            _offerService.AddOffer(offer);
            Receipt receipt = _checkoutService.ChecksOutArticlesFrom(_theCart);
            return Verifier.Verify(new ReceiptPrinter(40).PrintReceipt(receipt));
        }

        [Fact]
        public Task FiveForY_discount()
        {
            _theCart.AddItemQuantity(_apples, Quantity.ForProduct(_apples, 5M));
            var offer = _offerFactory.CreateFiveForAmountOffer(_apples, 6.99M);
            _offerService.AddOffer(offer);
            Receipt receipt = _checkoutService.ChecksOutArticlesFrom(_theCart);
            return Verifier.Verify(new ReceiptPrinter(40).PrintReceipt(receipt));
        }

        [Fact]
        public Task FiveForY_discount_withSix()
        {
            _theCart.AddItemQuantity(_apples, Quantity.ForProduct(_apples, 6M));
            var offer = _offerFactory.CreateFiveForAmountOffer(_apples, 6.99M);
            _offerService.AddOffer(offer);
            Receipt receipt = _checkoutService.ChecksOutArticlesFrom(_theCart);
            return Verifier.Verify(new ReceiptPrinter(40).PrintReceipt(receipt));
        }

        [Fact]
        public Task FiveForY_discount_withSixteen()
        {
            _theCart.AddItemQuantity(_apples, Quantity.ForProduct(_apples, 16M));
            var offer = _offerFactory.CreateFiveForAmountOffer(_apples, 6.99M);
            _offerService.AddOffer(offer);
            Receipt receipt = _checkoutService.ChecksOutArticlesFrom(_theCart);
            return Verifier.Verify(new ReceiptPrinter(40).PrintReceipt(receipt));
        }

        [Fact]
        public Task FiveForY_discount_withFour()
        {
            _theCart.AddItemQuantity(_apples, Quantity.ForProduct(_apples, 4M));
            var offer = _offerFactory.CreateFiveForAmountOffer(_apples, 6.99M);
            _offerService.AddOffer(offer);
            Receipt receipt = _checkoutService.ChecksOutArticlesFrom(_theCart);
            return Verifier.Verify(new ReceiptPrinter(40).PrintReceipt(receipt));
        }
    }
}