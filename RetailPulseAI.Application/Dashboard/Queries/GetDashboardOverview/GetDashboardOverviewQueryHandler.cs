using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using RetailPulseAI.Application.Abstractions.Repositories;
using RetailPulseAI.Application.DTOs;

namespace RetailPulseAI.Application.Dashboard.Queries.GetDashboardOverview;

public sealed class GetDashboardOverviewQueryHandler(
    IRetailAnalyticsRepository repository,
    IDistributedCache cache) : IRequestHandler<GetDashboardOverviewQuery, DashboardOverviewDto>
{
    public async Task<DashboardOverviewDto> Handle(GetDashboardOverviewQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"dashboard:{request.FromDate:yyyyMMdd}:{request.ToDate:yyyyMMdd}";
        var cached = await cache.GetStringAsync(cacheKey, cancellationToken);
        if (!string.IsNullOrWhiteSpace(cached))
        {
            return JsonSerializer.Deserialize<DashboardOverviewDto>(cached)!;
        }

        var result = await repository.GetDashboardOverviewAsync(request.FromDate, request.ToDate, cancellationToken);
        await cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(result), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
        }, cancellationToken);

        return result;
    }
}
