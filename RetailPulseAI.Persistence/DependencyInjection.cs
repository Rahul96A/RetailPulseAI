using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RetailPulseAI.Application.Abstractions;
using RetailPulseAI.Application.Abstractions.Repositories;
using RetailPulseAI.Persistence.Context;
using RetailPulseAI.Persistence.Repositories;

namespace RetailPulseAI.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<RetailPulseDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("RetailPulse")));

        services.AddScoped<IRetailAnalyticsRepository, RetailAnalyticsRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
