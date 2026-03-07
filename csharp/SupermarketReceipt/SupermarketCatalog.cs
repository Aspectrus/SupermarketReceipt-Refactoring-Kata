namespace SupermarketReceipt
{
    public interface SupermarketCatalog
    {
        void AddProduct(Product product, decimal price);

        decimal GetUnitPrice(Product product);
    }
}