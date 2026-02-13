using MediatR;
using RetailPulseAI.Application.DTOs;

namespace RetailPulseAI.Application.Analytics.Queries.GetPricingIndex;

public sealed record GetPricingIndexQuery(DateOnly FromDate, DateOnly ToDate) : IRequest<PricingIndexDto>;
