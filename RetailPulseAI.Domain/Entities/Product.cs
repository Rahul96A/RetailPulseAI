using RetailPulseAI.Domain.Common;

namespace RetailPulseAI.Domain.Entities;

public sealed class Product : BaseEntity
{
    public string Sku { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal Cost { get; set; }
}
