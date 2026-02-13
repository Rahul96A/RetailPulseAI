using MediatR;
using RetailPulseAI.Application.DTOs;

namespace RetailPulseAI.Application.Pricing.Commands.OptimizePricing;

public sealed record OptimizePricingCommand(decimal Cost, decimal CurrentPrice, decimal Elasticity, decimal CurrentUnits) : IRequest<PricingOptimizationResultDto>;
