using MediatR;
using RetailPulseAI.Application.DTOs;

namespace RetailPulseAI.Application.Dashboard.Queries.GetDashboardOverview;

public sealed record GetDashboardOverviewQuery(DateOnly FromDate, DateOnly ToDate) : IRequest<DashboardOverviewDto>;
