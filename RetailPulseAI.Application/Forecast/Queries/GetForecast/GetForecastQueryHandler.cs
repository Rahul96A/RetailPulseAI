using MediatR;
using RetailPulseAI.Application.Abstractions.Repositories;
using RetailPulseAI.Application.Abstractions.Services;
using RetailPulseAI.Application.DTOs;

namespace RetailPulseAI.Application.Forecast.Queries.GetForecast;

public sealed class GetForecastQueryHandler(
    IRetailAnalyticsRepository repository,
    IDemandForecastService demandForecastService) : IRequestHandler<GetForecastQuery, IReadOnlyCollection<ForecastPointDto>>
{
    public async Task<IReadOnlyCollection<ForecastPointDto>> Handle(GetForecastQuery request, CancellationToken cancellationToken)
    {
        var history = await repository.GetSalesHistoryBySkuAsync(request.Sku, cancellationToken);
        return await demandForecastService.ForecastNextFourWeeksAsync(request.Sku, history, cancellationToken);
    }
}
