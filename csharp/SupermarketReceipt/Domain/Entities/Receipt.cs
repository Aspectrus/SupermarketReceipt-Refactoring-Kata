using SupermarketReceipt.Domain.ValueObjects;
using System.Collections.Generic;

namespace SupermarketReceipt.Domain.Entities
{
    public class Receipt
    {
        private readonly List<Discount> _discounts = new List<Discount>();
        private readonly List<ReceiptItem> _items = new List<ReceiptItem>();


        public IReadOnlyList<ReceiptItem> GetItems() => _items.AsReadOnly();

        public IReadOnlyList<Discount> GetDiscounts() => _discounts.AsReadOnly();
        public decimal GetTotalPrice()
        {
            var total = 0.0M;
            foreach (var item in _items) total += item.TotalPrice;
            foreach (var discount in _discounts) total -= discount.DiscountAmount;
            return total;
        }

        public void AddProduct(Product product, Quantity quantity, decimal unitPrice)
        {
            _items.Add(new ReceiptItem(product, quantity, unitPrice, unitPrice * quantity.Amount));
        }


        public void AddDiscount(Discount discount)
        {
            _discounts.Add(discount);
        }
    }
}