using Microsoft.AspNetCore.Mvc;
using backend.BusinessLogic;
using backend.Dto;
using backend.Models;

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
        return visitors.Select(visitor =>
        {
            var visitorDto = ShortVisitorDto.FromModel(visitor.visitor);
            visitorDto.OpenBill = visitor.openBill;
            return visitorDto;
        }).ToList();
    }
}
