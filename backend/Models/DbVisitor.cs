namespace backend.Models;

public class DbVisitor
{
    public int Id { get; set; }
    public string? BadgeNumber { get; set; }
    public string? Name { get; set; }
    public int TariffId { get; set; }
    public DateTime OpenDateTime { get; set; }
    public int OpenedByUserId { get; set; }
    public DateTime? CloseDateTime { get; set; }
    public int? ClosedByUserId { get; set; }
    public int? PaymentAcceptedByUserId { get; set; }
    public decimal? Billed { get; set; }
    public decimal? Paid { get; set; }
}

public enum VisitorStatus
{
    Open = 0,
    Closed = 1,
    Paid = 2,
}

public class FullVisitor
{
    public FullVisitor(DbVisitor visitor)
    {
        DbVisitor = visitor;
    }

    public DbVisitor DbVisitor { get; set; }
    public VisitorStatus Status { get; set; }
    public decimal? OpenBill { get; set; }
    public TimeSpan? OpenDuration { get; set; }
    public TimeSpan? ClosedDuration { get; set; }
}
