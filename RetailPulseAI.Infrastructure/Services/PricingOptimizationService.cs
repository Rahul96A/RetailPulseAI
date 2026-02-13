using RetailPulseAI.Application.Abstractions.Services;
using RetailPulseAI.Application.DTOs;

namespace RetailPulseAI.Infrastructure.Services;

public sealed class PricingOptimizationService : IPricingOptimizationService
{
    public PricingOptimizationResultDto Optimize(decimal cost, decimal currentPrice, decimal elasticity, decimal currentUnits)
    {
        var marginGuardPrice = cost * 1.15m;
        var candidate = currentPrice * (1 - (elasticity / 10));
        var optimalPrice = Math.Max(marginGuardPrice, candidate);

        var unitChangeFactor = 1 + ((currentPrice - optimalPrice) / currentPrice) * Math.Abs(elasticity);
        var projectedUnits = Math.Max(0, currentUnits * unitChangeFactor);
        var projectedRevenue = projectedUnits * optimalPrice;

        return new PricingOptimizationResultDto(Math.Round(optimalPrice, 2), Math.Round(projectedRevenue, 2), Math.Round(projectedUnits, 0));
    }
}
