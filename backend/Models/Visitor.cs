namespace backend.Models;

public class Visitor
{
    public int Id { get; set; }
    public string? BadgeNumber { get; set; }
    public string? Name { get; set; }
    public DateTime OpenDateTime { get; set; }
    public int OpenedByUserId { get; set; }
    public DateTime? CloseDateTime { get; set; }
    public int? ClosedByUserId { get; set; }
    public int? PaymentAcceptedByUserId { get; set; }
    public decimal? Billed { get; set; }
    public decimal? Payed { get; set; }
}
