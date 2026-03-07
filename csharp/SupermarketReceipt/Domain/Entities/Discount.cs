using SupermarketReceipt.Domain.Entities.Offers;

namespace SupermarketReceipt.Domain.Entities
{
    public class Discount
    {
        public Discount(Product product, decimal discountAmount, SpecialOfferType offerType, decimal[] arguments)
        {
            Product = product;
            DiscountAmount = discountAmount;
            OfferType = offerType;
            Arguments = arguments;
        }

        public SpecialOfferType OfferType { get; }
        public decimal[] Arguments { get; }
        public decimal DiscountAmount { get; }
        public Product Product { get; }
    }
}