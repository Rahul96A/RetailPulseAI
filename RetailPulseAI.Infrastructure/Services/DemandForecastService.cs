using RetailPulseAI.Application.Abstractions.Services;
using RetailPulseAI.Application.DTOs;

namespace RetailPulseAI.Infrastructure.Services;

public sealed class DemandForecastService : IDemandForecastService
{
    public Task<IReadOnlyCollection<ForecastPointDto>> ForecastNextFourWeeksAsync(string sku, IReadOnlyCollection<SalesHistoryPointDto> history, CancellationToken cancellationToken)
    {
        var avg = history.Count == 0 ? 100 : (int)Math.Round(history.Average(x => x.UnitsSold));
        var start = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(7 - (int)DateTime.UtcNow.DayOfWeek);

        IReadOnlyCollection<ForecastPointDto> forecast = Enumerable.Range(0, 4)
            .Select(week => new ForecastPointDto(start.AddDays(week * 7), avg + (week * 3)))
            .ToList();

        return Task.FromResult(forecast);
    }
}
