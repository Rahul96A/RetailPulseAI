using MediatR;
using RetailPulseAI.Application.Abstractions.Repositories;
using RetailPulseAI.Application.DTOs;

namespace RetailPulseAI.Application.Analytics.Queries.GetAcv;

public sealed class GetAcvQueryHandler(IRetailAnalyticsRepository repository) : IRequestHandler<GetAcvQuery, AcvDistributionDto>
{
    public Task<AcvDistributionDto> Handle(GetAcvQuery request, CancellationToken cancellationToken) =>
        repository.GetAcvDistributionAsync(request.FromDate, request.ToDate, cancellationToken);
}
