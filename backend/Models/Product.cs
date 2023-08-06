namespace backend.Models;

public record DbProduct
{
    public int Id { get; init; }
    public DateTime CreatedDateTime { get; set; }
    public int CreatedByUserId { get; set; }
    public int Order { get; init; }
    public string Name { get; init; } = "";
    public decimal Price { get; init; }
    public bool InStock { get; set; }
}

public record NewProduct
{
    public required string Name { get; init; }
    public decimal Price { get; init; }
}

public record UpdateProduct
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required decimal Price { get; init; }
}

public record UpdateProductInStock
{
    public required int Id { get; init; }
    public required bool InStock { get; init; }
}

public record UpdateProductOrder
{
    public required int Id { get; init; }
    public required int Order { get; init; }
}
