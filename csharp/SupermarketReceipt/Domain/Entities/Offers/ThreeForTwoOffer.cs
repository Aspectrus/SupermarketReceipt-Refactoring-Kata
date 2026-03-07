using SupermarketReceipt.Domain.ValueObjects;

namespace SupermarketReceipt.Domain.Entities.Offers
{
    public class ThreeForTwoOffer : Offer
    {
        public override Discount CalculateDiscountForProduct(Product product, Quantity quantity, decimal unitPrice)
        {
            int totalUnits = (int)quantity.Amount;
            if (totalUnits < 3)
                return null;

            int groups = totalUnits / 3;
            int remainder = totalUnits % 3;

            decimal totalWithoutDiscount = unitPrice * totalUnits;
            decimal totalWithOffer = groups * 2 * unitPrice + remainder * unitPrice;
            decimal discount = totalWithoutDiscount - totalWithOffer;

            return discount > 0
                ? new Discount(product, "3 for 2", -discount)
                : null;
        }
    }
}