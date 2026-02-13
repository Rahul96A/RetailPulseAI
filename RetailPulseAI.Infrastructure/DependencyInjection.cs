using Microsoft.Extensions.DependencyInjection;
using RetailPulseAI.Application.Abstractions.Services;
using RetailPulseAI.Domain.Interfaces;
using RetailPulseAI.Infrastructure.BackgroundJobs;
using RetailPulseAI.Infrastructure.MultiTenancy;
using RetailPulseAI.Infrastructure.Services;

namespace RetailPulseAI.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ITenantContext, TenantContext>();
        services.AddScoped<IDemandForecastService, DemandForecastService>();
        services.AddScoped<IPricingOptimizationService, PricingOptimizationService>();
        services.AddScoped<IPromotionLiftService, PromotionLiftService>();

        services.AddHostedService<AnalyticsRefreshWorker>();
        return services;
    }
}
