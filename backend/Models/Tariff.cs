namespace backend.Models;

public class Tariff
{
    public int Id { get; set; }
    public bool IsActual { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public int CreatedByUserId { get; set; }
    public decimal PricePerHourFirstHour { get; set; }
    public decimal? PricePerHourSecondHour { get; set; }
    public decimal? PricePerHourThirdHour { get; set; }
    public decimal? MaxBillTotalTime { get; set; }
}
