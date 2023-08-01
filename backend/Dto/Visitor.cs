using backend.Models;

namespace backend.Dto;

public class ShortVisitorDto
{
    public int Id { get; set; }
    public string? BadgeNumber { get; set; }
    public string? Name { get; set; }
    public int TariffId { get; set; }
    public DateTime OpenDateTime { get; set; }
    public DateTime? CloseDateTime { get; set; }
    public decimal? Billed { get; set; }
    public decimal? Paid { get; set; }
    public string? Status { get; set; }
    public TimeSpan? OpenDuration { get; set; }
    public decimal? OpenBill { get; set; }
    public TimeSpan? ClosedDuration { get; set; }

    public static ShortVisitorDto FromModel(FullVisitor visitor)
    {
        return new ShortVisitorDto
        {
            Id = visitor.DbVisitor.Id,
            BadgeNumber = visitor.DbVisitor.BadgeNumber,
            Name = visitor.DbVisitor.Name,
            TariffId = visitor.DbVisitor.TariffId,
            OpenDateTime = visitor.DbVisitor.OpenDateTime,
            CloseDateTime = visitor.DbVisitor.CloseDateTime,
            Billed = visitor.DbVisitor.Billed,
            Paid = visitor.DbVisitor.Paid,
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

    public DbVisitor ToModel() =>
        new DbVisitor()
        {
            BadgeNumber = BadgeNumber,
            Name = Name,
        };
}

public class CloseVisitorDto
{
    public int Id { get; set; }

    public DbVisitor ToModel() =>
        new DbVisitor()
        {
            Id = Id,
        };
}

public class PaidVisitorDto
{
    public int Id { get; set; }
    public decimal Paid { get; set; }

    public DbVisitor ToModel() =>
        new DbVisitor()
        {
            Id = Id,
            Paid = Paid,
        };
}
