using FluentValidation;

namespace RetailPulseAI.Application.Sales.Queries.GetSalesTrends;

public sealed class GetSalesTrendsQueryValidator : AbstractValidator<GetSalesTrendsQuery>
{
    public GetSalesTrendsQueryValidator()
    {
        RuleFor(x => x.Page).GreaterThan(0);
        RuleFor(x => x.PageSize).InclusiveBetween(1, 365);
    }
}
