namespace RetailPulseAI.Application.DTOs;

public sealed record DashboardOverviewDto(decimal TotalRevenue, int TotalUnits, decimal GrowthPercent, decimal MarketShare);

public sealed record SalesTrendPointDto(DateOnly Date, decimal Revenue, int UnitsSold);

public sealed record MarketShareDto(string Brand, string Category, decimal BrandSales, decimal CategorySales, decimal MarketSharePercent);

public sealed record AcvDistributionDto(decimal WeightedDistributionPercent, int TotalStores, int CarryingStores);

public sealed record PricingIndexDto(decimal OwnAveragePrice, decimal CompetitorAveragePrice, decimal PriceIndexPercent);

public sealed record SalesHistoryPointDto(DateOnly Date, int UnitsSold);

public sealed record ForecastPointDto(DateOnly WeekStartDate, int ForecastUnits);

public sealed record PricingOptimizationRequestDto(decimal Cost, decimal CurrentPrice, decimal Elasticity, decimal CurrentUnits);

public sealed record PricingOptimizationResultDto(decimal OptimalPrice, decimal ProjectedRevenue, decimal ProjectedUnits);

public sealed record PromotionLiftDto(decimal BaselineRevenue, decimal PromoRevenue);

public sealed record PromotionLiftSummaryDto(decimal BaselineRevenue, decimal PromoRevenue, decimal LiftAmount, decimal LiftPercent);

public sealed record PagedResponse<T>(IReadOnlyCollection<T> Items, int Page, int PageSize);
