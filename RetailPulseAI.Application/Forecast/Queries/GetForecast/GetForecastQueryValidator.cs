using FluentValidation;

namespace RetailPulseAI.Application.Forecast.Queries.GetForecast;

public sealed class GetForecastQueryValidator : AbstractValidator<GetForecastQuery>
{
    public GetForecastQueryValidator()
    {
        RuleFor(x => x.Sku).NotEmpty().MaximumLength(50);
    }
}
