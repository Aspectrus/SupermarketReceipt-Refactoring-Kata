using System;
using SupermarketReceipt.Domain.Entities;
using SupermarketReceipt.Domain.ValueObjects;
using Xunit;

namespace SupermarketReceipt.Tests.Domain.ValueObjects
{
    public class CatalogEntryTests
    {
        [Fact]
        public void Constructor_ValidParameters_ShouldSetProperties()
        {
            // Arrange
            var product = new Product("Apple", ProductUnit.Each);
            var price = 1.99m;

            // Act
            var entry = new CatalogEntry(product, price);

            // Assert
            Assert.Same(product, entry.Product);
            Assert.Equal(price, entry.Price);
        }

        [Fact]
        public void Constructor_ProductIsNull_ShouldThrowArgumentNullException()
        {
            // Arrange
            Product product = null;
            var price = 1.99m;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new CatalogEntry(product, price));
        }

        [Theory]
        [InlineData(0.0)]
        [InlineData(-0.01)]
        [InlineData(-10.5)]
        public void Constructor_PriceNonPositive_ShouldThrowArgumentOutOfRangeException(decimal invalidPrice)
        {
            // Arrange
            var product = new Product("Apple", ProductUnit.Each);

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new CatalogEntry(product, invalidPrice));
        }

        [Fact]
        public void Constructor_PriceZero_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            var product = new Product("Apple", ProductUnit.Each);
            var price = 0m;

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new CatalogEntry(product, price));
        }
    }
}