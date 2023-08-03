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
