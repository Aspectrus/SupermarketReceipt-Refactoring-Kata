using SupermarketReceipt.Domain.Entities.Offers;
using System.Globalization;
using System.Linq;

namespace SupermarketReceipt.Test
{
    public class ReceiptFormatter
    {
        public string GetDiscountDescription(SpecialOfferType offerType, decimal[] arguments, CultureInfo culture)
        {
            object[] objectArgs = arguments.Cast<object>().ToArray();
            return offerType switch
            {
                SpecialOfferType.TwoForAmount => string.Format(culture, "{0:0} for {1:N2}", objectArgs),
                SpecialOfferType.FiveForAmount => string.Format(culture, "{0:0} for {1:N2}", objectArgs),
                SpecialOfferType.TenPercentDiscount => string.Format(culture, "{0:0.##}% off", objectArgs),
                SpecialOfferType.ThreeForTwo => string.Format(culture, "{0:0} for {1:0}", objectArgs),
                _ => "Unknown offer"
            };
        }
    }
}
