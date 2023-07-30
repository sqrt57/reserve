using backend.Models;

namespace backend.BusinessLogic;

public class VisitorsService
{
    public Task<IReadOnlyCollection<(Visitor visitor, decimal openBill)>> GetOpenVisitors()
    {
        var visitor = new Visitor
        {
            Id = 1,
            BadgeNumber = "57",
            OpenDateTime = DateTime.Now.Subtract(TimeSpan.FromMinutes(33)),
            OpenedByUserId = 5,
        };

        return Task.FromResult<IReadOnlyCollection<(Visitor visitor, decimal openBill)>>(new[] {(visitor, 10m)});
    }
}
