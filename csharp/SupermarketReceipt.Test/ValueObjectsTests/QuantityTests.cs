using System;
using SupermarketReceipt.Domain.Entities;
using SupermarketReceipt.Domain.ValueObjects;
using Xunit;

namespace SupermarketReceipt.Tests.Domain.ValueObjects
{
    public class QuantityTests
    {
        [Fact]
        public void ForProduct_WithKiloProduct_ValidKilos_CreatesQuantity()
        {
            // Arrange
            var product = new Product("Flour", ProductUnit.Kilo);
            decimal kilos = 2.5m;

            // Act
            var quantity = Quantity.ForProduct(product, kilos);

            // Assert
            Assert.Equal(kilos, quantity.Amount);
            Assert.Equal(ProductUnit.Kilo, quantity.Unit);
        }

        [Fact]
        public void ForProduct_WithKiloProduct_DefaultAmount_CreatesQuantity()
        {
            // Arrange
            var product = new Product("Flour", ProductUnit.Kilo);

            // Act
            var quantity = Quantity.ForProduct(product);

            // Assert
            Assert.Equal(1.0m, quantity.Amount);
            Assert.Equal(ProductUnit.Kilo, quantity.Unit);
        }

        [Theory]
        [InlineData(0.0)]
        [InlineData(-0.5)]
        [InlineData(-10)]
        public void ForProduct_WithKiloProduct_NonPositiveKilos_ThrowsArgumentException(decimal invalidKilos)
        {
            // Arrange
            var product = new Product("Flour", ProductUnit.Kilo);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => Quantity.ForProduct(product, invalidKilos));
        }

        [Fact]
        public void ForProduct_WithEachProduct_DefaultAmount_CreatesQuantity()
        {
            // Arrange
            var product = new Product("Apple", ProductUnit.Each);

            // Act
            var quantity = Quantity.ForProduct(product);

            // Assert
            Assert.Equal(1.0m, quantity.Amount);
            Assert.Equal(ProductUnit.Each, quantity.Unit);
        }

        [Theory]
        [InlineData(0.0)]
        [InlineData(-1)]
        [InlineData(-2.5)]
        public void ForProduct_WithEachProduct_NonPositiveUnits_ThrowsArgumentException(decimal invalidUnits)
        {
            // Arrange
            var product = new Product("Apple", ProductUnit.Each);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => Quantity.ForProduct(product, invalidUnits));
        }

        [Theory]
        [InlineData(1.5)]
        [InlineData(2.3)]
        [InlineData(3.99)]
        public void ForProduct_WithEachProduct_NonIntegerUnits_ThrowsArgumentException(decimal nonIntegerUnits)
        {
            // Arrange
            var product = new Product("Apple", ProductUnit.Each);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => Quantity.ForProduct(product, nonIntegerUnits));
        }

        [Fact]
        public void Add_WithNullOther_ThrowsArgumentNullException()
        {
            // Arrange
            var quantity = Quantity.ForProduct(new Product("Apple", ProductUnit.Each), 2);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => quantity.Add(null));
        }

        [Fact]
        public void Add_WithDifferentUnits_ThrowsInvalidOperationException()
        {
            // Arrange
            var quantityKilo = Quantity.ForProduct(new Product("Flour", ProductUnit.Kilo), 1.5m);
            var quantityEach = Quantity.ForProduct(new Product("Apple", ProductUnit.Each), 2);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => quantityKilo.Add(quantityEach));
        }

        [Fact]
        public void Add_WithSameUnits_ReturnsSummedQuantity()
        {
            // Arrange
            var q1 = Quantity.ForProduct(new Product("Apple", ProductUnit.Each), 2);
            var q2 = Quantity.ForProduct(new Product("Apple", ProductUnit.Each), 3);

            // Act
            var result = q1.Add(q2);

            // Assert
            Assert.Equal(5m, result.Amount);
            Assert.Equal(ProductUnit.Each, result.Unit);
        }
    }
}