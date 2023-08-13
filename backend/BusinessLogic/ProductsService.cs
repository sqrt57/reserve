using backend.DbStores;
using backend.Exceptions;
using backend.Models;
using backend.Services;

namespace backend.BusinessLogic;

public class ProductsService
{
    private readonly ProductsStore _productsStore;
    private readonly UserIdAccessor _userIdAccessor;

    public ProductsService(
        ProductsStore productsStore,
        UserIdAccessor userIdAccessor)
    {
        _productsStore = productsStore;
        _userIdAccessor = userIdAccessor;
    }

    public async Task<IReadOnlyCollection<DbProduct>> GetAll()
    {
        return await _productsStore.GetAllProducts();
    }

    public async Task<IReadOnlyCollection<DbProduct>> GetInStock()
    {
        var allProducts = await _productsStore.GetAllProducts();
        return allProducts.Where(p => p.InStock).ToList();
    }

    public async Task<DbProduct> CreateNew(NewProduct newProduct)
    {
        var now = DateTime.Now;
        var dbProduct = new DbProduct
        {
            Name = newProduct.Name,
            Price = newProduct.Price,
            InStock = true,
            CreatedDateTime = now,
            CreatedByUserId = _userIdAccessor.GetUserId(),
        };
        var result = await _productsStore.InsertWithMaxOrder(dbProduct);
        if (result == null)
            throw new DbInsertException();
        return result;
    }

    public async Task<DbProduct> Update(UpdateProduct updateProduct)
    {
        var now = DateTime.Now;
        var dbProduct = new DbProduct
        {
            Id = updateProduct.Id,
            Name = updateProduct.Name,
            Price = updateProduct.Price,
            InStock = true,
            CreatedDateTime = now,
            CreatedByUserId = _userIdAccessor.GetUserId(),
        };
        var result = await _productsStore.UpdateProduct(dbProduct);
        if (result == null)
            throw new DbUpdateException();
        return result;
    }

    public async Task<DbProduct?> UpdateInStock(UpdateProductInStock updateProduct)
    {
        throw new NotImplementedException();
    }

    public async Task<DbProduct?> UpdateOrder(UpdateProductOrder[] productOrders)
    {
        throw new NotImplementedException();
    }
}
