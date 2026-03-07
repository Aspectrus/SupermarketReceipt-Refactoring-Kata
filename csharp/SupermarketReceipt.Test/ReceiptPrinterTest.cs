using SupermarketReceipt.Domain.Entities;
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
            _receipt.AddProduct(_toothbrush, 1m, 0.99m, 0.99m);
            return Verifier.Verify(new ReceiptPrinter().PrintReceipt(_receipt));
        }

        [Fact]
        public Task quantityTwo()
        {
            _receipt.AddProduct(_toothbrush, 2m, 0.99m, 0.99m * 2m);
            return Verifier.Verify(new ReceiptPrinter().PrintReceipt(_receipt));
        }

        [Fact]
        public Task looseWeight()
        {
            _receipt.AddProduct(_apples, 2.3m, 1.99m, 1.99m * 2.3m);
            return Verifier.Verify(new ReceiptPrinter().PrintReceipt(_receipt));
        }

        [Fact]
        public Task total()
        {
            _receipt.AddProduct(_toothbrush, 1m, 0.99m, 2m * 0.99m);
            _receipt.AddProduct(_apples, 0.75m, 1.99m, 1.99m * 0.75m);
            return Verifier.Verify(new ReceiptPrinter().PrintReceipt(_receipt));
        }

        [Fact]
        public Task discounts()
        {
            _receipt.AddDiscount(new Discount(_apples, "3 for 2", 0.99m));
            return Verifier.Verify(new ReceiptPrinter().PrintReceipt(_receipt));
        }

        [Fact]
        public Task printWholeReceipt()
        {
            _receipt.AddProduct(_toothbrush, 1m, 0.99m, 0.99m);
            _receipt.AddProduct(_toothbrush, 2m, 0.99m, 2m * 0.99m);
            _receipt.AddProduct(_apples, 0.75m, 1.99m, 1.99m * 0.75m);
            _receipt.AddDiscount(new Discount(_toothbrush, "3 for 2", 0.99m));
            return Verifier.Verify(new ReceiptPrinter().PrintReceipt(_receipt));
        }
    }
}