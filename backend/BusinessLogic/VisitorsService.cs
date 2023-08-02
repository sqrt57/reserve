using backend.DbStores;
using backend.Exceptions;
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

    public async Task<IReadOnlyCollection<FullVisitor>> GetOpenVisitors()
    {
        var now = DateTime.Now;
        var visitorTariffs = await _visitorsStore.GetOpenVisitors(now.AddMinutes(-10));
        var result = visitorTariffs
            .Select(visitorTariff => EnrichVisitor(visitorTariff.Visitor, visitorTariff.Tariff, now))
            .ToList();
        return result;
    }

    public async Task<FullVisitor> NewVisitor(DbVisitor visitor)
    {
        var now = DateTime.Now;
        visitor.OpenDateTime = now;
        visitor.OpenedByUserId = _userIdAccessor.GetUserId();
        var resultVisitor = await _visitorsStore.CreateVisitor(visitor);
        return EnrichVisitor(resultVisitor, null, now);
    }

    public async Task<FullVisitor> CloseVisitor(DbVisitor visitor)
    {
        var now = DateTime.Now;
        var visitorTariff = await _visitorsStore.GetVisitorById(visitor.Id);
        if (visitorTariff == null)
            throw new EntityNotFoundException();

        var dbVisitor = visitorTariff.Value.Visitor;
        var dbTariff = visitorTariff.Value.Tariff;

        if (dbVisitor.CloseDateTime == null)
        {
            dbVisitor.CloseDateTime = now;
            dbVisitor.ClosedByUserId = _userIdAccessor.GetUserId();
            dbVisitor.Billed = CalculateBill(dbTariff, now - dbVisitor.OpenDateTime);
        }

        var successful = await _visitorsStore.UpdateVisitor(dbVisitor);
        if (!successful)
            throw new DbUpdateException();

        return EnrichVisitor(dbVisitor, null, now);
    }

    public async Task<FullVisitor> PaidVisitor(DbVisitor visitor)
    {
        var now = DateTime.Now;
        var visitorTariff = await _visitorsStore.GetVisitorById(visitor.Id);
        if (visitorTariff == null)
            throw new EntityNotFoundException();

        var dbVisitor = visitorTariff.Value.Visitor;
        var dbTariff = visitorTariff.Value.Tariff;

        dbVisitor.Paid = visitor.Paid;

        var successful = await _visitorsStore.UpdateVisitor(dbVisitor);
        if (!successful)
            throw new DbUpdateException();

        return EnrichVisitor(dbVisitor, dbTariff, now);
    }

    private static FullVisitor EnrichVisitor(DbVisitor visitor, DbTariff? tariff, DateTime now)
    {
        var status = visitor.CloseDateTime == null
            ? VisitorStatus.Open
            : visitor.Paid == null
                ? VisitorStatus.Closed
                : VisitorStatus.Paid;

        var result = new FullVisitor(visitor) {Status = status};

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

    private static decimal? CalculateBill(DbTariff? tariff, TimeSpan openDuration)
    {
        if (tariff == null)
            return null;

        var firstHour = tariff.FirstHour;
        var secondHour = tariff.SecondHour ?? firstHour;
        var thirdHour = tariff.ThirdHour ?? secondHour;
        var fourthHour = tariff.FourthHour ?? thirdHour;

        var totalHours = (decimal) openDuration.TotalHours;

        var result = totalHours switch
        {
            <= 0 => 0,
            <= 1 => firstHour * totalHours,
            <= 2 => firstHour + secondHour * (totalHours - 1),
            <= 3 => firstHour + secondHour + thirdHour * (totalHours - 2),
            _ => firstHour + secondHour + thirdHour + fourthHour * (totalHours - 2)
        };

        if (tariff.MaxTimeBill.HasValue && result > tariff.MaxTimeBill.Value)
            result = tariff.MaxTimeBill.Value;

        return result;
    }
}
