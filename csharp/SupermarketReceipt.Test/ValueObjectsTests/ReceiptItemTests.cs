using System;
using SupermarketReceipt.Domain.Entities;
using SupermarketReceipt.Domain.ValueObjects;
using Xunit;

namespace SupermarketReceipt.Tests.Domain.ValueObjects
{
    public class ReceiptItemTests
    {
        [Fact]
        public void Constructor_ValidParameters_SetsProperties()
        {
            // Arrange
            var product = new Product("Apple", ProductUnit.Each);
            var quantity = Quantity.ForProduct(product, 3);
            var price = 1.50m;
            var totalPrice = price * quantity.Amount; 

            // Act
            var item = new ReceiptItem(product, quantity, price, totalPrice);

            // Assert
            Assert.Same(product, item.Product);
            Assert.Same(quantity, item.Quantity);
            Assert.Equal(price, item.Price);
            Assert.Equal(totalPrice, item.TotalPrice);
        }

        [Fact]
        public void Constructor_ProductNull_ThrowsArgumentNullException()
        {
            // Arrange
            Product product = null;
            var quantity = Quantity.ForProduct(new Product("Apple", ProductUnit.Each), 3);
            var price = 1.50m;
            var totalPrice = price * quantity.Amount;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ReceiptItem(product, quantity, price, totalPrice));
        }

        [Fact]
        public void Constructor_QuantityNull_ThrowsArgumentNullException()
        {
            // Arrange
            var product = new Product("Apple", ProductUnit.Each);
            Quantity quantity = null;
            var price = 1.50m;
            var totalPrice = price * 3;

            // Act & Assert
           Assert.Throws<ArgumentNullException>(() => new ReceiptItem(product, quantity, price, 4.50m));
        }

        [Theory]
        [InlineData(-0.01)]
        [InlineData(-10.0)]
        public void Constructor_NegativePrice_ThrowsArgumentOutOfRangeException(decimal negativePrice)
        {
            // Arrange
            var product = new Product("Apple", ProductUnit.Each);
            var quantity = Quantity.ForProduct(product, 3);
            var totalPrice = 4.50m;

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new ReceiptItem(product, quantity, negativePrice, totalPrice));
        }

        [Fact]
        public void Constructor_PriceZero_IsAllowed()
        {
            // Arrange
            var product = new Product("Apple", ProductUnit.Each);
            var quantity = Quantity.ForProduct(product, 3);
            var price = 0m;
            var totalPrice = price * quantity.Amount;

            // Act
            var item = new ReceiptItem(product, quantity, price, totalPrice);

            // Assert
            Assert.Equal(0m, item.Price);
            Assert.Equal(0m, item.TotalPrice);
        }

        [Theory]
        [InlineData(1.5, 3, 4.49)]
        [InlineData(1.5, 3, 4.51)]
        [InlineData(2.0, 2.5, 4.0)]
        public void Constructor_TotalPriceMismatch_ThrowsArgumentException(decimal price, decimal quantityAmount, decimal invalidTotalPrice)
        {
            // Arrange
            var product = new Product("Apple", quantityAmount % 1 == 0 ? ProductUnit.Each : ProductUnit.Kilo);
            var quantity = Quantity.ForProduct(product, quantityAmount);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new ReceiptItem(product, quantity, price, invalidTotalPrice));
        }

        [Fact]
        public void Constructor_TotalPriceMatches_DoesNotThrow()
        {
            // Arrange
            var product = new Product("Flour", ProductUnit.Kilo);
            var quantity = Quantity.ForProduct(product, 2.5m);
            var price = 3.0m;
            var totalPrice = price * quantity.Amount;

            // Act
            var item = new ReceiptItem(product, quantity, price, totalPrice);

            // Assert
            Assert.Equal(totalPrice, item.TotalPrice);
        }
    }
}