using RetailPulseAI.Application.DTOs;

namespace RetailPulseAI.Application.Abstractions.Services;

public interface IDemandForecastService
{
    Task<IReadOnlyCollection<ForecastPointDto>> ForecastNextFourWeeksAsync(string sku, IReadOnlyCollection<SalesHistoryPointDto> history, CancellationToken cancellationToken);
}
