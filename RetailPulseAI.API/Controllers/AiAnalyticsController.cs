using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetailPulseAI.Application.Pricing.Commands.OptimizePricing;
using RetailPulseAI.Application.Promotions.Queries.GetPromotionLift;
using RetailPulseAI.Application.Forecast.Queries.GetForecast;

namespace RetailPulseAI.API.Controllers;

[ApiController]
[Route("api")]
[Authorize(Policy = "AnalyticsRead")]
public sealed class AiAnalyticsController(IMediator mediator) : ControllerBase
{
    [HttpGet("forecast/{sku}")]
    public async Task<IActionResult> GetForecast([FromRoute] string sku, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetForecastQuery(sku), cancellationToken));

    [HttpPost("pricing/optimize")]
    [Authorize(Policy = "PricingWrite")]
    public async Task<IActionResult> OptimizePricing([FromBody] OptimizePricingCommand command, CancellationToken cancellationToken)
        => Ok(await mediator.Send(command, cancellationToken));

    [HttpGet("promotions/lift")]
    public async Task<IActionResult> GetPromotionLift([FromQuery] Guid? productId, [FromQuery] DateOnly fromDate, [FromQuery] DateOnly toDate, CancellationToken cancellationToken)
        => Ok(await mediator.Send(new GetPromotionLiftQuery(productId, fromDate, toDate), cancellationToken));
}
