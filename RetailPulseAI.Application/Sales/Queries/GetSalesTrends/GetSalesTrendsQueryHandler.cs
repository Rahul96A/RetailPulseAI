using MediatR;
using RetailPulseAI.Application.Abstractions.Repositories;
using RetailPulseAI.Application.DTOs;

namespace RetailPulseAI.Application.Sales.Queries.GetSalesTrends;

public sealed class GetSalesTrendsQueryHandler(IRetailAnalyticsRepository repository) : IRequestHandler<GetSalesTrendsQuery, PagedResponse<SalesTrendPointDto>>
{
    public async Task<PagedResponse<SalesTrendPointDto>> Handle(GetSalesTrendsQuery request, CancellationToken cancellationToken)
    {
        var items = await repository.GetSalesTrendsAsync(request.FromDate, request.ToDate, request.Page, request.PageSize, cancellationToken);
        return new PagedResponse<SalesTrendPointDto>(items, request.Page, request.PageSize);
    }
}
