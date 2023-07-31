using Microsoft.AspNetCore.Mvc;
using backend.BusinessLogic;
using backend.Dto;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VisitorsController
{
    private readonly VisitorsService _visitorsService;

    public VisitorsController(VisitorsService visitorsService)
    {
        _visitorsService = visitorsService;
    }

    [HttpGet]
    public async Task<IReadOnlyCollection<ShortVisitorDto>> Get()
    {
        var visitors = await _visitorsService.GetOpenVisitors();
        return visitors.Select(ShortVisitorDto.FromModel).ToList();
    }

    [HttpPost()]
    [Route("new")]
    public async Task<ShortVisitorDto> NewVisitor(NewVisitorDto newVisitor)
    {
        var resultVisitor = await _visitorsService.NewVisitor(newVisitor.ToModel());
        return ShortVisitorDto.FromModel(resultVisitor);
    }

    [HttpPost()]
    [Route("close")]
    public async Task<ShortVisitorDto> CloseVisitor(CloseVisitorDto data)
    {
        var resultVisitor = await _visitorsService.CloseVisitor(data.ToModel());
        return ShortVisitorDto.FromModel(resultVisitor);
    }

    [HttpPost()]
    [Route("paid")]
    public async Task<ShortVisitorDto> PaidVisitor(PaidVisitorDto data)
    {
        var resultVisitor = await _visitorsService.PaidVisitor(data.ToModel());
        return ShortVisitorDto.FromModel(resultVisitor);
    }
}
