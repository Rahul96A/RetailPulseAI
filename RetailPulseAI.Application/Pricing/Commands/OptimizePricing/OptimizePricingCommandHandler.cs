using MediatR;
using RetailPulseAI.Application.Abstractions.Services;
using RetailPulseAI.Application.DTOs;

namespace RetailPulseAI.Application.Pricing.Commands.OptimizePricing;

public sealed class OptimizePricingCommandHandler(IPricingOptimizationService pricingOptimizationService) : IRequestHandler<OptimizePricingCommand, PricingOptimizationResultDto>
{
    public Task<PricingOptimizationResultDto> Handle(OptimizePricingCommand request, CancellationToken cancellationToken)
    {
        var result = pricingOptimizationService.Optimize(request.Cost, request.CurrentPrice, request.Elasticity, request.CurrentUnits);
        return Task.FromResult(result);
    }
}
