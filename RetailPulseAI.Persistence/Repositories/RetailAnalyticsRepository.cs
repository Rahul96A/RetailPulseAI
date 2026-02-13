using Microsoft.EntityFrameworkCore;
using RetailPulseAI.Application.Abstractions.Repositories;
using RetailPulseAI.Application.DTOs;
using RetailPulseAI.Persistence.Context;

namespace RetailPulseAI.Persistence.Repositories;

public sealed class RetailAnalyticsRepository(RetailPulseDbContext dbContext) : IRetailAnalyticsRepository
{
    public async Task<DashboardOverviewDto> GetDashboardOverviewAsync(DateOnly fromDate, DateOnly toDate, CancellationToken cancellationToken)
    {
        var current = await dbContext.RetailSales
            .Where(x => x.Date >= fromDate && x.Date <= toDate)
            .GroupBy(_ => 1)
            .Select(g => new { Revenue = g.Sum(x => x.Revenue), Units = g.Sum(x => x.UnitsSold) })
            .FirstOrDefaultAsync(cancellationToken);

        var days = toDate.DayNumber - fromDate.DayNumber + 1;
        var previousFrom = fromDate.AddDays(-days);
        var previousTo = fromDate.AddDays(-1);

        var previousRevenue = await dbContext.RetailSales
            .Where(x => x.Date >= previousFrom && x.Date <= previousTo)
            .SumAsync(x => (decimal?)x.Revenue, cancellationToken) ?? 0m;

        var currentRevenue = current?.Revenue ?? 0m;
        var growth = previousRevenue == 0 ? 100m : ((currentRevenue - previousRevenue) / previousRevenue) * 100m;

        var share = await GetMarketShareAsync("RetailPulse", "Beverages", fromDate, toDate, cancellationToken);
        return new DashboardOverviewDto(currentRevenue, current?.Units ?? 0, Math.Round(growth, 2), share.MarketSharePercent);
    }

    public async Task<IReadOnlyCollection<SalesTrendPointDto>> GetSalesTrendsAsync(DateOnly fromDate, DateOnly toDate, int page, int pageSize, CancellationToken cancellationToken)
    {
        return await dbContext.RetailSales
            .Where(x => x.Date >= fromDate && x.Date <= toDate)
            .OrderBy(x => x.Date)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new SalesTrendPointDto(x.Date, x.Revenue, x.UnitsSold))
            .ToListAsync(cancellationToken);
    }

    public async Task<MarketShareDto> GetMarketShareAsync(string brand, string category, DateOnly fromDate, DateOnly toDate, CancellationToken cancellationToken)
    {
        var query = dbContext.RetailSales.Include(x => x.Product).Where(x => x.Date >= fromDate && x.Date <= toDate);
        var brandSales = await query.Where(x => x.Product!.Brand == brand).SumAsync(x => (decimal?)x.Revenue, cancellationToken) ?? 0m;
        var categorySales = await query.Where(x => x.Product!.Category == category).SumAsync(x => (decimal?)x.Revenue, cancellationToken) ?? 0m;
        var percent = categorySales == 0 ? 0 : (brandSales / categorySales) * 100m;

        return new MarketShareDto(brand, category, brandSales, categorySales, Math.Round(percent, 2));
    }

    public async Task<AcvDistributionDto> GetAcvDistributionAsync(DateOnly fromDate, DateOnly toDate, CancellationToken cancellationToken)
    {
        var totalStores = await dbContext.Stores.CountAsync(cancellationToken);
        var carryingStores = await dbContext.RetailSales
            .Where(x => x.Date >= fromDate && x.Date <= toDate)
            .Select(x => x.StoreId)
            .Distinct()
            .CountAsync(cancellationToken);

        var distribution = totalStores == 0 ? 0 : ((decimal)carryingStores / totalStores) * 100m;
        return new AcvDistributionDto(Math.Round(distribution, 2), totalStores, carryingStores);
    }

    public async Task<PricingIndexDto> GetPricingIndexAsync(DateOnly fromDate, DateOnly toDate, CancellationToken cancellationToken)
    {
        var ownPrice = await dbContext.RetailSales
            .Where(x => x.Date >= fromDate && x.Date <= toDate)
            .AverageAsync(x => (decimal?)x.Price, cancellationToken) ?? 0m;

        var competitorPrice = await dbContext.CompetitorPrices
            .Where(x => x.Date >= fromDate && x.Date <= toDate)
            .AverageAsync(x => (decimal?)x.Price, cancellationToken) ?? 0m;

        var index = competitorPrice == 0 ? 0 : (ownPrice / competitorPrice) * 100m;
        return new PricingIndexDto(Math.Round(ownPrice, 2), Math.Round(competitorPrice, 2), Math.Round(index, 2));
    }

    public async Task<IReadOnlyCollection<SalesHistoryPointDto>> GetSalesHistoryBySkuAsync(string sku, CancellationToken cancellationToken)
    {
        return await dbContext.RetailSales
            .Include(x => x.Product)
            .Where(x => x.Product!.Sku == sku)
            .OrderBy(x => x.Date)
            .Select(x => new SalesHistoryPointDto(x.Date, x.UnitsSold))
            .ToListAsync(cancellationToken);
    }

    public async Task<PromotionLiftDto> GetPromotionLiftAsync(Guid? productId, DateOnly fromDate, DateOnly toDate, CancellationToken cancellationToken)
    {
        var sales = dbContext.RetailSales.Where(x => x.Date >= fromDate && x.Date <= toDate);
        if (productId.HasValue)
        {
            sales = sales.Where(x => x.ProductId == productId.Value);
        }

        var avgDaily = await sales.GroupBy(x => x.Date).Select(g => g.Sum(x => x.Revenue)).DefaultIfEmpty(0m).AverageAsync(cancellationToken);

        var promoRevenue = await (from p in dbContext.Promotions
                                 join s in dbContext.RetailSales on p.ProductId equals s.ProductId
                                 where s.Date >= p.StartDate && s.Date <= p.EndDate
                                 select s.Revenue).DefaultIfEmpty(0m).SumAsync(cancellationToken);

        return new PromotionLiftDto(Math.Round(avgDaily, 2), Math.Round(promoRevenue, 2));
    }
}
