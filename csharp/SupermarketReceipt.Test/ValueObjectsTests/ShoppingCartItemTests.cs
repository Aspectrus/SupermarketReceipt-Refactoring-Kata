using System;
using SupermarketReceipt.Domain.Entities;
using SupermarketReceipt.Domain.ValueObjects;
using Xunit;

namespace SupermarketReceipt.Tests.Domain.ValueObjects
{
    public class ShoppingCartItemTests
    {
        [Fact]
        public void Constructor_ValidParameters_SetsProperties()
        {
            // Arrange
            var product = new Product("Apple", ProductUnit.Each);
            var quantity = Quantity.ForProduct(product, 3);

            // Act
            var item = new ShoppingCartItem(product, quantity);

            // Assert
            Assert.Same(product, item.Product);
            Assert.Same(quantity, item.Quantity);
        }

        [Fact]
        public void Constructor_ProductIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            Product product = null;
            var quantity = Quantity.ForProduct(new Product("Apple", ProductUnit.Each), 3);

            // Act & Assert
           Assert.Throws<ArgumentNullException>(() => new ShoppingCartItem(product, quantity));
        }

        [Fact]
        public void Constructor_QuantityIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            var product = new Product("Apple", ProductUnit.Each);
            Quantity quantity = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new ShoppingCartItem(product, quantity));
        }
        [Fact]
        public void Constructor_QuantityUnitDoesNotMatchProductUnit_ThrowsArgumentException()
        {
            // Arrange
            var product = new Product("Apple", ProductUnit.Each);
            var quantity = Quantity.ForProduct(new Product("Flour", ProductUnit.Kilo), 2.5m);

            // Act & Assert
           Assert.Throws<ArgumentException>(() => new ShoppingCartItem(product, quantity));

        }
    }
}