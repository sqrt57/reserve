using backend.DbStores;
using backend.Models;
using backend.Services;

namespace backend.BusinessLogic;

public class VisitorsService
{
    private readonly VisitorsStore _visitorsStore;
    private readonly TariffsStore _tariffsStore;
    private readonly UserIdAccessor _userIdAccessor;

    public VisitorsService(
        VisitorsStore visitorsStore,
        TariffsStore tariffsStore,
        UserIdAccessor userIdAccessor)
    {
        _visitorsStore = visitorsStore;
        _tariffsStore = tariffsStore;
        _userIdAccessor = userIdAccessor;
    }

    public async Task<IReadOnlyCollection<OpenVisitor>> GetOpenVisitors()
    {
        var now = DateTime.Now;
        var visitors = await _visitorsStore.GetOpenVisitors(now.AddMinutes(-10));
        var tariff = await _tariffsStore.GetTariff();
        var result = visitors.Select(visitor => EnrichVisitor(visitor, tariff, now)).ToList();
        return result;
    }

    public async Task<OpenVisitor> NewVisitor(Visitor visitor)
    {
        var now = DateTime.Now;
        visitor.OpenDateTime = now;
        visitor.OpenedByUserId = _userIdAccessor.GetUserId();
        var resultVisitor = await _visitorsStore.CreateVisitor(visitor);
        return EnrichVisitor(resultVisitor, null, now);
    }

    private OpenVisitor EnrichVisitor(Visitor visitor, Tariff? tariff, DateTime now)
    {
        var status = visitor.CloseDateTime == null
            ? VisitorStatus.Open
            : visitor.Payed == null
                ? VisitorStatus.Closed
                : VisitorStatus.Payed;

        var result = new OpenVisitor(visitor) {Status = status};

        if (status == VisitorStatus.Open)
        {
            var openDuration = now - visitor.OpenDateTime;
            result.OpenDuration = openDuration;
            result.OpenBill = CalculateBill(tariff, openDuration);
        }

        if (status != VisitorStatus.Open)
        {
            result.ClosedDuration = visitor.CloseDateTime - visitor.OpenDateTime;
        }

        return result;
    }

    private decimal? CalculateBill(Tariff? tariff, TimeSpan openDuration)
    {
        if (tariff == null)
            return null;

        var firstHour =  tariff.PricePerHourFirstHour;
        var secondHour = tariff.PricePerHourSecondHour ?? firstHour;
        var thirdHour = tariff.PricePerHourThirdHour ?? secondHour;

        decimal result;
        var totalHours = (decimal) openDuration.TotalHours;
        if (totalHours < 0)
            result = 0;
        else if (openDuration.TotalHours <= 1)
            result = firstHour * totalHours;
        else if (openDuration.TotalHours <= 2)
            result = firstHour + secondHour * (totalHours - 1);
        else
            result = firstHour + secondHour + thirdHour * (totalHours - 2);

        if (tariff.MaxBillTotalTime.HasValue && result > tariff.MaxBillTotalTime.Value)
            result = tariff.MaxBillTotalTime.Value;

        return result;
    }
}
