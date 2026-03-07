using SupermarketReceipt.Domain.Entities;
using SupermarketReceipt.Domain.Entities.Offers;
using SupermarketReceipt.Domain.ValueObjects;
using System.Threading.Tasks;
using VerifyXunit;
using Xunit;

namespace SupermarketReceipt.Test
{
    public class ReceiptPrinterTest
    {
        readonly Product _toothbrush = new Product("toothbrush", ProductUnit.Each);
        readonly Product _apples = new Product("apples", ProductUnit.Kilo);
        Receipt _receipt = new Receipt();

        [Fact]
        public Task oneLineItem()
        {
            _receipt.AddProduct(_toothbrush, Quantity.ForProduct(_toothbrush), 0.99m);
            return Verifier.Verify(new ReceiptPrinter().PrintReceipt(_receipt));
        }

        [Fact]
        public Task quantityTwo()
        {
            _receipt.AddProduct(_toothbrush, Quantity.ForProduct(_toothbrush, 2), 0.99m);
            return Verifier.Verify(new ReceiptPrinter().PrintReceipt(_receipt));
        }

        [Fact]
        public Task looseWeight()
        {
            _receipt.AddProduct(_apples, Quantity.ForProduct(_apples, 2.3M), 1.99m);
            return Verifier.Verify(new ReceiptPrinter().PrintReceipt(_receipt));
        }

        [Fact]
        public Task total()
        {
            _receipt.AddProduct(_toothbrush, Quantity.ForProduct(_toothbrush, 1M), 0.99m);
            _receipt.AddProduct(_apples, Quantity.ForProduct(_apples, 0.75M), 1.99m);
            return Verifier.Verify(new ReceiptPrinter().PrintReceipt(_receipt));
        }

        [Fact]
        public Task discounts()
        {
            _receipt.AddDiscount(
                new Discount(
                    _apples,
                    0.99M,
                    SpecialOfferType.ThreeForTwo,
                    [3, 2]
                )
            ); return Verifier.Verify(new ReceiptPrinter().PrintReceipt(_receipt));
        }

        [Fact]
        public Task printWholeReceipt()
        {
            _receipt.AddProduct(_toothbrush, Quantity.ForProduct(_toothbrush, 1M), 0.99M);
            _receipt.AddProduct(_toothbrush, Quantity.ForProduct(_toothbrush, 2M), 0.99M);
            _receipt.AddProduct(_apples, Quantity.ForProduct(_apples, 0.75M), 1.99M);
            _receipt.AddDiscount(
                new Discount(
                    _toothbrush,
                    0.99M,
                    SpecialOfferType.ThreeForTwo,
                     [3, 2]
                )
            ); return Verifier.Verify(new ReceiptPrinter().PrintReceipt(_receipt));
        }
    }
}