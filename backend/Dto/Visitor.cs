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
    public decimal? Paid { get; set; }
    public string? Status { get; set; }
    public TimeSpan? OpenDuration { get; set; }
    public decimal? OpenBill { get; set; }
    public TimeSpan? ClosedDuration { get; set; }

    public static ShortVisitorDto FromModel(OpenVisitor visitor)
    {
        return new ShortVisitorDto
        {
            Id = visitor.Visitor.Id,
            BadgeNumber = visitor.Visitor.BadgeNumber,
            Name = visitor.Visitor.Name,
            OpenDateTime = visitor.Visitor.OpenDateTime,
            CloseDateTime = visitor.Visitor.CloseDateTime,
            Billed = visitor.Visitor.Billed,
            Paid = visitor.Visitor.Paid,
            Status = visitor.Status.ToString(),
            OpenDuration = visitor.OpenDuration,
            OpenBill = visitor.OpenBill,
            ClosedDuration = visitor.ClosedDuration,
        };
    }
}

public class NewVisitorDto
{
    public string? BadgeNumber { get; set; }
    public string? Name { get; set; }

    public Visitor ToModel() =>
        new Visitor()
        {
            BadgeNumber = BadgeNumber,
            Name = Name,
        };
}

public class CloseVisitorDto
{
    public int Id { get; set; }

    public Visitor ToModel() =>
        new Visitor()
        {
            Id = Id,
        };
}

public class PaidVisitorDto
{
    public int Id { get; set; }
    public decimal Paid { get; set; }

    public Visitor ToModel() =>
        new Visitor()
        {
            Id = Id,
            Paid = Paid,
        };
}
