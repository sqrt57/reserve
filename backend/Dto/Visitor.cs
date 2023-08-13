using backend.Models;

namespace backend.Dto;

public record ShortVisitorDto
{
    public required int Id { get; init; }
    public required string? BadgeNumber { get; init; }
    public required string? Name { get; init; }
    public required int TariffId { get; init; }
    public required DateTime OpenDateTime { get; init; }
    public required DateTime? CloseDateTime { get; init; }
    public required decimal? Billed { get; init; }
    public required decimal? Paid { get; init; }
    public required string Status { get; init; }
    public required double? DurationSeconds { get; init; }

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
            Billed = visitor.Billed,
            Paid = visitor.DbVisitor.Paid,
            Status = visitor.Status.ToString(),
            DurationSeconds = visitor.Duration?.TotalSeconds,
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
