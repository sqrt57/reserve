namespace backend.Models;

public class DbTariff
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Order { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public int CreatedByUserId { get; set; }
    public decimal FirstHour { get; set; }
    public decimal? SecondHour { get; set; }
    public decimal? ThirdHour { get; set; }
    public decimal? FourthHour { get; set; }
    public decimal? MaxTimeBill { get; set; }
}
