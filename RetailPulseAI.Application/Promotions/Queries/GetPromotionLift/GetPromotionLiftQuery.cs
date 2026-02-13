using MediatR;
using RetailPulseAI.Application.DTOs;

namespace RetailPulseAI.Application.Promotions.Queries.GetPromotionLift;

public sealed record GetPromotionLiftQuery(Guid? ProductId, DateOnly FromDate, DateOnly ToDate) : IRequest<PromotionLiftSummaryDto>;
