using backend.Models;

namespace backend.Dto;

public record ProductDto
{
    public required int Id { get; init; }
    public required DateTime CreatedDateTime { get; set; }
    public required int CreatedByUserId { get; set; }
    public required int Order { get; init; }
    public required string Name { get; init; } = "";
    public required decimal Price { get; init; }
    public required bool InStock { get; set; }

    public static ProductDto FromModel(DbProduct product) =>
        new ProductDto
        {
            Id = product.Id,
            CreatedDateTime = product.CreatedDateTime,
            CreatedByUserId = product.CreatedByUserId,
            Order = product.Order,
            Name = product.Name,
            Price = product.Price,
            InStock = product.InStock,
        };
}

public record NewProductDto
{
    public required string Name { get; init; }
    public decimal Price { get; init; }

    public NewProduct ToModel() =>
        new()
        {
            Name = Name,
            Price = Price,
        };
}

public record UpdateProductDto
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required decimal Price { get; init; }

    public UpdateProduct ToModel() =>
        new()
        {
            Id = Id,
            Name = Name,
            Price = Price,
        };
}

public record UpdateProductInStockDto
{
    public required int Id { get; init; }
    public required bool InStock { get; init; }

    public UpdateProductInStock ToModel() =>
        new()
        {
            Id = Id,
            InStock = InStock,
        };
}

public record UpdateProductOrderDto
{
    public required int Id { get; init; }
    public required int Order { get; init; }

    public UpdateProductOrder ToModel() =>
        new()
        {
            Id = Id,
            Order = Order,
        };
}

public record DeleteProductDto
{
    public required int Id { get; init; }

}
