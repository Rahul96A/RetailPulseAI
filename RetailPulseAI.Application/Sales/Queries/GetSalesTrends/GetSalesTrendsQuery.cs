using MediatR;
using RetailPulseAI.Application.DTOs;

namespace RetailPulseAI.Application.Sales.Queries.GetSalesTrends;

public sealed record GetSalesTrendsQuery(DateOnly FromDate, DateOnly ToDate, int Page = 1, int PageSize = 30) : IRequest<PagedResponse<SalesTrendPointDto>>;
