using MediatR;
using RetailPulseAI.Application.Abstractions.Repositories;
using RetailPulseAI.Application.DTOs;

namespace RetailPulseAI.Application.Analytics.Queries.GetPricingIndex;

public sealed class GetPricingIndexQueryHandler(IRetailAnalyticsRepository repository) : IRequestHandler<GetPricingIndexQuery, PricingIndexDto>
{
    public Task<PricingIndexDto> Handle(GetPricingIndexQuery request, CancellationToken cancellationToken) =>
        repository.GetPricingIndexAsync(request.FromDate, request.ToDate, cancellationToken);
}
