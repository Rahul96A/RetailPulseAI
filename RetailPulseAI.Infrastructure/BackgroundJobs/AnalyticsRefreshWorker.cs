using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace RetailPulseAI.Infrastructure.BackgroundJobs;

public sealed class AnalyticsRefreshWorker(ILogger<AnalyticsRefreshWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Analytics refresh heartbeat at {UtcNow}", DateTime.UtcNow);
            await Task.Delay(TimeSpan.FromMinutes(15), stoppingToken);
        }
    }
}
