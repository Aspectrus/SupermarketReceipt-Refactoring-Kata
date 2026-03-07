using System.Collections.Generic;

namespace SupermarketReceipt
{
    public class Receipt
    {
        private readonly List<Discount> _discounts = new List<Discount>();
        private readonly List<ReceiptItem> _items = new List<ReceiptItem>();

        public decimal GetTotalPrice()
        {
            var total = 0.0M;
            foreach (var item in _items) total += item.TotalPrice;
            foreach (var discount in _discounts) total += discount.DiscountAmount;
            return total;
        }

        public void AddProduct(Product p, decimal quantity, decimal price, decimal totalPrice)
        {
            _items.Add(new ReceiptItem(p, quantity, price, totalPrice));
        }

        public List<ReceiptItem> GetItems()
        {
            return new List<ReceiptItem>(_items);
        }

        public void AddDiscount(Discount discount)
        {
            _discounts.Add(discount);
        }

        public List<Discount> GetDiscounts()
        {
            return _discounts;
        }
    }

    public class ReceiptItem
    {
        public ReceiptItem(Product p, decimal quantity, decimal price, decimal totalPrice)
        {
            Product = p;
            Quantity = quantity;
            Price = price;
            TotalPrice = totalPrice;
        }

        public Product Product { get; }
        public decimal Price { get; }
        public decimal TotalPrice { get; }
        public decimal Quantity { get; }
    }
}