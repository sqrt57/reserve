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

public class TariffDto
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required int Order { get; set; }
    public required decimal FirstHour { get; init; }
    public required decimal? SecondHour { get; init; }
    public required decimal? ThirdHour { get; init; }
    public required decimal? FourthHour { get; init; }
    public required decimal? MaxTimeBill { get; init; }

    public static TariffDto FromModel(DbTariff tariff)
    {
        return new TariffDto
        {
            Id = tariff.Id,
            Name = tariff.Name,
            Order = tariff.Order,
            FirstHour = tariff.FirstHour,
            SecondHour = tariff.SecondHour,
            ThirdHour = tariff.ThirdHour,
            FourthHour = tariff.FourthHour,
            MaxTimeBill = tariff.MaxTimeBill,
        };
    }
}

public class NewVisitorDto
{
    public string? BadgeNumber { get; set; }
    public string? Name { get; set; }
    public int TariffId { get; set; }

    public NewVisitor ToModel() =>
        new NewVisitor()
        {
            BadgeNumber = BadgeNumber,
            Name = Name,
            TariffId = TariffId,
        };
}

public class CloseVisitorDto
{
    public int Id { get; set; }

    public CloseVisitor ToModel() =>
        new CloseVisitor()
        {
            Id = Id,
        };
}

public class PaidVisitorDto
{
    public int Id { get; set; }
    public decimal Paid { get; set; }

    public PaidVisitor ToModel() =>
        new PaidVisitor
        {
            Id = Id,
            Paid = Paid,
        };
}

public class VisitorsIndexDto
{
    public required List<ShortVisitorDto> Visitors { get; init; }
    public required List<TariffDto> Tariffs { get; init; }
}
