using MediatR;
using RetailPulseAI.Application.DTOs;

namespace RetailPulseAI.Application.Forecast.Queries.GetForecast;

public sealed record GetForecastQuery(string Sku) : IRequest<IReadOnlyCollection<ForecastPointDto>>;
