using backend.DbStores;
using backend.Models;

namespace backend.BusinessLogic;

public class ProductsService
{
    private readonly ProductsStore _productsStore;

    public ProductsService(ProductsStore productsStore)
    {
        _productsStore = productsStore;
    }

    public async Task<IReadOnlyCollection<DbProduct>> GetAllProducts()
    {
        return await _productsStore.GetAllProducts();
    }
}
