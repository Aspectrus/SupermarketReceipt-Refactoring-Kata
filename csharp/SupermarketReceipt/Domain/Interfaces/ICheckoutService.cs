using SupermarketReceipt.Domain.Entities;

namespace SupermarketReceipt.Domain.Interfaces
{
    public interface ICheckoutService
    {
        Receipt ChecksOutArticlesFrom(ShoppingCart cart);
    }
}