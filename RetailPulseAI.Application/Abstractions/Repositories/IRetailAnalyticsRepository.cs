using RetailPulseAI.Application.DTOs;

namespace RetailPulseAI.Application.Abstractions.Repositories;

public interface IRetailAnalyticsRepository
{
    Task<DashboardOverviewDto> GetDashboardOverviewAsync(DateOnly fromDate, DateOnly toDate, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<SalesTrendPointDto>> GetSalesTrendsAsync(DateOnly fromDate, DateOnly toDate, int page, int pageSize, CancellationToken cancellationToken);
    Task<MarketShareDto> GetMarketShareAsync(string brand, string category, DateOnly fromDate, DateOnly toDate, CancellationToken cancellationToken);
    Task<AcvDistributionDto> GetAcvDistributionAsync(DateOnly fromDate, DateOnly toDate, CancellationToken cancellationToken);
    Task<PricingIndexDto> GetPricingIndexAsync(DateOnly fromDate, DateOnly toDate, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<SalesHistoryPointDto>> GetSalesHistoryBySkuAsync(string sku, CancellationToken cancellationToken);
    Task<PromotionLiftDto> GetPromotionLiftAsync(Guid? productId, DateOnly fromDate, DateOnly toDate, CancellationToken cancellationToken);
}
