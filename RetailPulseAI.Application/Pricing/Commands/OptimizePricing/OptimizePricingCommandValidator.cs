using FluentValidation;

namespace RetailPulseAI.Application.Pricing.Commands.OptimizePricing;

public sealed class OptimizePricingCommandValidator : AbstractValidator<OptimizePricingCommand>
{
    public OptimizePricingCommandValidator()
    {
        RuleFor(x => x.Cost).GreaterThanOrEqualTo(0);
        RuleFor(x => x.CurrentPrice).GreaterThan(0);
        RuleFor(x => x.Elasticity).LessThan(0);
        RuleFor(x => x.CurrentUnits).GreaterThan(0);
    }
}
