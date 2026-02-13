using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetailPulseAI.Application.Analytics.Queries.GetAcv;
using RetailPulseAI.Application.Analytics.Queries.GetMarketShare;
using RetailPulseAI.Application.Analytics.Queries.GetPricingIndex;
using RetailPulseAI.Application.Dashboard.Queries.GetDashboardOverview;
using RetailPulseAI.Application.Sales.Queries.GetSalesTrends;

namespace RetailPulseAI.API.Controllers;

[ApiController]
[Route("api")]
[Authorize(Policy = "AnalyticsRead")]
public sealed class DashboardController(IMediator mediator) : ControllerBase
{
    [HttpGet("dashboard/overview")]
    public async Task<IActionResult> GetOverview([FromQuery] DateOnly fromDate, [FromQuery] DateOnly toDate, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetDashboardOverviewQuery(fromDate, toDate), cancellationToken));

    [HttpGet("sales/trends")]
    public async Task<IActionResult> GetSalesTrends([FromQuery] DateOnly fromDate, [FromQuery] DateOnly toDate, [FromQuery] int page = 1, [FromQuery] int pageSize = 30, CancellationToken cancellationToken = default)
        => Ok(await mediator.Send(new GetSalesTrendsQuery(fromDate, toDate, page, pageSize), cancellationToken));

    [HttpGet("marketshare")]
    public async Task<IActionResult> GetMarketShare([FromQuery] string brand, [FromQuery] string category, [FromQuery] DateOnly fromDate, [FromQuery] DateOnly toDate, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetMarketShareQuery(brand, category, fromDate, toDate), cancellationToken));

    [HttpGet("acv")]
    public async Task<IActionResult> GetAcv([FromQuery] DateOnly fromDate, [FromQuery] DateOnly toDate, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetAcvQuery(fromDate, toDate), cancellationToken));

    [HttpGet("pricing/index")]
    public async Task<IActionResult> GetPricingIndex([FromQuery] DateOnly fromDate, [FromQuery] DateOnly toDate, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetPricingIndexQuery(fromDate, toDate), cancellationToken));
}
