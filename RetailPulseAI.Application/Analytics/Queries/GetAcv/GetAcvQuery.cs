using MediatR;
using RetailPulseAI.Application.DTOs;

namespace RetailPulseAI.Application.Analytics.Queries.GetAcv;

public sealed record GetAcvQuery(DateOnly FromDate, DateOnly ToDate) : IRequest<AcvDistributionDto>;
