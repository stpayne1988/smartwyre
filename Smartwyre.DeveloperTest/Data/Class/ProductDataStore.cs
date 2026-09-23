using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data;

public class ProductDataStore : IProductRepository
{
    public Product GetProduct(string productIdentifier)
    {
        // Access database to retrieve product, code removed for brevity 
        return new Product();
    }
}
