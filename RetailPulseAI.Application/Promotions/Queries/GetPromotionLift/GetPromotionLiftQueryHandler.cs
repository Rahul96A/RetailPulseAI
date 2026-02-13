using MediatR;
using RetailPulseAI.Application.Abstractions.Repositories;
using RetailPulseAI.Application.Abstractions.Services;
using RetailPulseAI.Application.DTOs;

namespace RetailPulseAI.Application.Promotions.Queries.GetPromotionLift;

public sealed class GetPromotionLiftQueryHandler(
    IRetailAnalyticsRepository repository,
    IPromotionLiftService promotionLiftService) : IRequestHandler<GetPromotionLiftQuery, PromotionLiftSummaryDto>
{
    public async Task<PromotionLiftSummaryDto> Handle(GetPromotionLiftQuery request, CancellationToken cancellationToken)
    {
        var input = await repository.GetPromotionLiftAsync(request.ProductId, request.FromDate, request.ToDate, cancellationToken);
        return promotionLiftService.CalculateLift(input);
    }
}
