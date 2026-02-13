using RetailPulseAI.Domain.Common;

namespace RetailPulseAI.Domain.Entities;

public sealed class Promotion : BaseEntity
{
    public Guid ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public decimal DiscountPercent { get; set; }

    public Product? Product { get; set; }
}
