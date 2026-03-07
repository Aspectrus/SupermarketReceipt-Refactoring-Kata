using System;
using SupermarketReceipt.Domain.Entities;
using SupermarketReceipt.Domain.ValueObjects;
using Xunit;

namespace SupermarketReceipt.Tests.Domain.Entities
{
    public class ProductTests
    {
        [Theory]
        [InlineData("Apple", ProductUnit.Each)]
        [InlineData("Flour", ProductUnit.Kilo)]
        [InlineData("Milk", ProductUnit.Each)]
        public void Constructor_ValidParameters_SetsProperties(string name, ProductUnit unit)
        {
            // Act
            var product = new Product(name, unit);

            // Assert
            Assert.Equal(name, product.Name);
            Assert.Equal(unit, product.Unit);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_InvalidName_ThrowsArgumentException(string invalidName)
        {
            // Arrange
            var unit = ProductUnit.Each;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Product(invalidName, unit));
        }

        [Fact]
        public void Constructor_InvalidUnit_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var invalidUnit = (ProductUnit)999;

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => new Product("Test", invalidUnit));
        }

        [Fact]
        public void Equals_EqualProducts_ReturnsTrue()
        {
            // Arrange
            var product1 = new Product("Apple", ProductUnit.Each);
            var product2 = new Product("Apple", ProductUnit.Each);

            // Act
            var result = product1.Equals(product2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equals_DifferentName_ReturnsFalse()
        {
            // Arrange
            var product1 = new Product("Apple", ProductUnit.Each);
            var product2 = new Product("Orange", ProductUnit.Each);

            // Act
            var result = product1.Equals(product2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_Null_ReturnsFalse()
        {
            // Arrange
            var product = new Product("Apple", ProductUnit.Each);

            // Act
            var result = product.Equals(null);

            // Assert
            Assert.False(result);
        }
    }
}