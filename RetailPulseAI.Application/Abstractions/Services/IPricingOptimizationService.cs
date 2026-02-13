using RetailPulseAI.Application.DTOs;

namespace RetailPulseAI.Application.Abstractions.Services;

public interface IPricingOptimizationService
{
    PricingOptimizationResultDto Optimize(decimal cost, decimal currentPrice, decimal elasticity, decimal currentUnits);
}
