using RetailPulseAI.Application.DTOs;

namespace RetailPulseAI.Application.Abstractions.Services;

public interface IPromotionLiftService
{
    PromotionLiftSummaryDto CalculateLift(PromotionLiftDto input);
}
