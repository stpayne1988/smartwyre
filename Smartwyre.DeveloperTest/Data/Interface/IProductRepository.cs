using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data;

public interface IProductRepository
{
    Product GetProduct(string productIdentifier);
}
