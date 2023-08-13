namespace backend.Models;

public record DbVisitor
{
    public int Id { get; init; }
    public string? BadgeNumber { get; init; }
    public string? Name { get; init; }
    public int TariffId { get; init; }
    public DateTime OpenDateTime { get; init; }
    public int OpenedByUserId { get; init; }
    public DateTime? CloseDateTime { get; init; }
    public int? ClosedByUserId { get; init; }
    public int? PaymentAcceptedByUserId { get; init; }
    public decimal? Billed { get; init; }
    public decimal? Paid { get; init; }
}

public enum VisitorStatus
{
    Open = 0,
    Closed = 1,
    Paid = 2,
}

public record FullVisitor
{
    public required DbVisitor DbVisitor { get; init; }
    public required VisitorStatus Status { get; init; }
    public required TimeSpan? Duration { get; init; }
    public required decimal? Billed { get; init; }
}

public record NewVisitor
{
    public required string? BadgeNumber { get; init; }
    public required string? Name { get; init; }
    public required int TariffId { get; init; }
}

public record CloseVisitor
{
    public required int Id { get; init; }
}

public record PaidVisitor
{
    public required int Id { get; init; }
    public required decimal Paid { get; init; }
}

