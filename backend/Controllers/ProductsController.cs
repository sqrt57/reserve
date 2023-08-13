﻿using Microsoft.AspNetCore.Mvc;
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
        var products = await _productsService.GetAll();
        return products.Select(ProductDto.FromModel).ToList();
    }

    [HttpGet]
    [Route("in-stock")]
    public async Task<IReadOnlyCollection<ProductDto>> GetInStock()
    {
        var products = await _productsService.GetInStock();
        return products.Select(ProductDto.FromModel).ToList();
    }

    [HttpPost]
    [Route("new")]
    public async Task<ProductDto> CreateNew(NewProductDto newProduct)
    {
        var product = await _productsService.CreateNew(newProduct.ToModel());
        return ProductDto.FromModel(product);
    }

    [HttpPost]
    [Route("update")]
    public async Task<ProductDto> Update(UpdateProductDto newProduct)
    {
        var product = await _productsService.Update(newProduct.ToModel());
        return ProductDto.FromModel(product);
    }
}
