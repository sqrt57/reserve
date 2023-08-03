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

    public async Task<FullVisitor> NewVisitor(NewVisitor visitor)
    {
        var now = DateTime.Now;
        var dbVisitor = new DbVisitor
        {
            BadgeNumber = visitor.BadgeNumber,
            Name = visitor.Name,
            TariffId = visitor.TariffId,
            OpenDateTime = now,
            OpenedByUserId = _userIdAccessor.GetUserId(),
        };
        var id = await _visitorsStore.CreateVisitor(dbVisitor);
        var resultVisitor = dbVisitor with {Id = id};
        return EnrichVisitor(resultVisitor, null, now);
    }

    public async Task<FullVisitor> CloseVisitor(CloseVisitor visitor)
    {
        var now = DateTime.Now;
        var visitorTariff = await _visitorsStore.GetVisitorById(visitor.Id);
        if (visitorTariff == null)
            throw new EntityNotFoundException();

        var dbVisitor = visitorTariff.Value.Visitor;
        var dbTariff = visitorTariff.Value.Tariff;

        if (dbVisitor.CloseDateTime != null)
            throw new BusinessLogicException();

        dbVisitor = dbVisitor with
        {
            CloseDateTime = now,
            ClosedByUserId = _userIdAccessor.GetUserId(),
            Billed = CalculateBill(dbTariff, now - dbVisitor.OpenDateTime),
        };

        var successful = await _visitorsStore.UpdateVisitor(dbVisitor);
        if (!successful)
            throw new DbUpdateException();

        return EnrichVisitor(dbVisitor, null, now);

    }

    public async Task<FullVisitor> PaidVisitor(PaidVisitor visitor)
    {
        var now = DateTime.Now;
        var visitorTariff = await _visitorsStore.GetVisitorById(visitor.Id);
        if (visitorTariff == null)
            throw new EntityNotFoundException();

        var dbVisitor = visitorTariff.Value.Visitor;
        var dbTariff = visitorTariff.Value.Tariff;

        dbVisitor = dbVisitor with {Paid = visitor.Paid};

        var successful = await _visitorsStore.UpdateVisitor(dbVisitor);
        if (!successful)
            throw new DbUpdateException();

        return EnrichVisitor(dbVisitor, dbTariff, now);
    }

    private static FullVisitor EnrichVisitor(DbVisitor visitor, DbTariff? tariff, DateTime now)
    {
        VisitorStatus status;
        TimeSpan? openDuration = null;
        Decimal? openBill = null;
        TimeSpan? closedDuration = null;

        if (visitor.CloseDateTime == null)
        {
            status = VisitorStatus.Open;
            openDuration = now - visitor.OpenDateTime;
            openBill = CalculateBill(tariff, openDuration.Value);
        }
        else if (visitor.Paid == null)
        {
            status = VisitorStatus.Closed;
            closedDuration = visitor.CloseDateTime - visitor.OpenDateTime;
        }
        else
        {
            status = VisitorStatus.Paid;
            closedDuration = visitor.CloseDateTime - visitor.OpenDateTime;

        }

        var result = new FullVisitor
        {
            DbVisitor = visitor,
            Status = status,
            OpenDuration = openDuration,
            OpenBill = openBill,
            ClosedDuration = closedDuration,
        };

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
