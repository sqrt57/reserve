using backend.Models;

namespace backend.Dto;

public class ShortVisitorDto
{
    public int Id { get; set; }
    public string? BadgeNumber { get; set; }
    public string? Name { get; set; }
    public DateTime OpenDateTime { get; set; }
    public DateTime? CloseDateTime { get; set; }
    public decimal? Billed { get; set; }
    public decimal? Payed { get; set; }
    public decimal? OpenBill { get; set; }

    public static ShortVisitorDto FromModel(Visitor visitor)
    {
        return new ShortVisitorDto
        {
            Id = visitor.Id,
            BadgeNumber = visitor.BadgeNumber,
            Name = visitor.Name,
            OpenDateTime = visitor.OpenDateTime,
            CloseDateTime = visitor.CloseDateTime,
            Billed = visitor.Billed,
            Payed = visitor.Payed,
        };
    }
}
