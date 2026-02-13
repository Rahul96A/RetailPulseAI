using MediatR;
using RetailPulseAI.Application.Abstractions.Repositories;
using RetailPulseAI.Application.DTOs;

namespace RetailPulseAI.Application.Analytics.Queries.GetMarketShare;

public sealed class GetMarketShareQueryHandler(IRetailAnalyticsRepository repository) : IRequestHandler<GetMarketShareQuery, MarketShareDto>
{
    public Task<MarketShareDto> Handle(GetMarketShareQuery request, CancellationToken cancellationToken) =>
        repository.GetMarketShareAsync(request.Brand, request.Category, request.FromDate, request.ToDate, cancellationToken);
}
