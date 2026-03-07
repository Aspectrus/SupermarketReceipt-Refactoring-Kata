using SupermarketReceipt.Domain.Entities;
using System;

namespace SupermarketReceipt.Domain.ValueObjects
{
    public enum ProductUnit
    {
        Kilo,
        Each
    }

    public class Quantity
    {
        public decimal Amount { get; }
        public ProductUnit Unit { get; }

        private Quantity(decimal amount, ProductUnit unit)
        {
            Amount = amount;
            Unit = unit;
        }

        private static Quantity ForKilos(decimal kilos)
        {
            if (kilos <= 0)
                throw new ArgumentException("Kilos must be positive", nameof(kilos));
            return new Quantity(kilos, ProductUnit.Kilo);
        }

        private static Quantity ForUnits(decimal units)
        {
            if (units <= 0 || units % 1m != 0m)
                throw new ArgumentException("Units must be positive whole numbers", nameof(units));
            return new Quantity(units, ProductUnit.Each);
        }

        public static Quantity ForProduct(Product product, decimal amount = 1.0m)
        {
            return product.Unit switch
            {
                ProductUnit.Each => ForUnits(amount),
                ProductUnit.Kilo => ForKilos(amount),
                _ => throw new NotSupportedException($"No default defined for {product.Unit}")
            };
        }

        public Quantity Add(Quantity other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            if (other.Unit != Unit)
                throw new InvalidOperationException($"Cannot add quantities of different units: {Unit} and {other.Unit}");
            var sum = Amount + other.Amount;
            if (sum < 0)
                throw new InvalidOperationException("Quantity cannot be negative");
            return new Quantity(Amount + other.Amount, Unit);
        }
    }
}