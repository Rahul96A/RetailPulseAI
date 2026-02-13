using MediatR;
using RetailPulseAI.Application.DTOs;

namespace RetailPulseAI.Application.Analytics.Queries.GetMarketShare;

public sealed record GetMarketShareQuery(string Brand, string Category, DateOnly FromDate, DateOnly ToDate) : IRequest<MarketShareDto>;
