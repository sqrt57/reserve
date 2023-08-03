using Microsoft.AspNetCore.Mvc;
using backend.BusinessLogic;
using backend.Dto;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController
{
    private readonly ProductsService _productsService;

    public ProductsController(ProductsService productsService)
    {
        _productsService = productsService;
    }

    [HttpGet]
    [Route("all")]
    public async Task<IReadOnlyCollection<ProductDto>> GetAll()
    {
        var products = await _productsService.GetAllProducts();
        return products.Select(ProductDto.FromModel).ToList();
    }

}
