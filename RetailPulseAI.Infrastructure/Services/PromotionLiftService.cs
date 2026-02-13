using RetailPulseAI.Application.Abstractions.Services;
using RetailPulseAI.Application.DTOs;

namespace RetailPulseAI.Infrastructure.Services;

public sealed class PromotionLiftService : IPromotionLiftService
{
    public PromotionLiftSummaryDto CalculateLift(PromotionLiftDto input)
    {
        var liftAmount = input.PromoRevenue - input.BaselineRevenue;
        var liftPercent = input.BaselineRevenue == 0 ? 0 : (liftAmount / input.BaselineRevenue) * 100m;
        return new PromotionLiftSummaryDto(input.BaselineRevenue, input.PromoRevenue, Math.Round(liftAmount, 2), Math.Round(liftPercent, 2));
    }
}
