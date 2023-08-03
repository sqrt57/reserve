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
