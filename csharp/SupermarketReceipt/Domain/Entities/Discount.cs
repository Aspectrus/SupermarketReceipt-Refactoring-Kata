namespace SupermarketReceipt.Domain.Entities
{
    public class Discount
    {
        public Discount(Product product, string description, decimal discountAmount)
        {
            Product = product;
            Description = description;
            DiscountAmount = discountAmount;
        }

        public string Description { get; }
        public decimal DiscountAmount { get; }
        public Product Product { get; }
    }
}