using Microsoft.AspNetCore.Mvc;
using backend.BusinessLogic;
using backend.Dto;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VisitorsController
{
    private readonly VisitorsService _visitorsService;
    private readonly TariffsService _tariffsService;

    public VisitorsController(VisitorsService visitorsService, TariffsService tariffsService)
    {
        _visitorsService = visitorsService;
        _tariffsService = tariffsService;
    }

    [HttpGet]
    public async Task<VisitorsIndexDto> Get()
    {
        var visitors = await _visitorsService.GetOpenVisitors();
        var tariffs = await _tariffsService.GetTariffs();
        return new VisitorsIndexDto()
        {
            Visitors = visitors.Select(ShortVisitorDto.FromModel).ToList(),
            Tariffs = tariffs.Select(TariffDto.FromModel).ToList(),
        };
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
