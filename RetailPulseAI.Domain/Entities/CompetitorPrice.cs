using RetailPulseAI.Domain.Common;

namespace RetailPulseAI.Domain.Entities;

public sealed class CompetitorPrice : BaseEntity
{
    public Guid ProductId { get; set; }
    public string CompetitorName { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public decimal Price { get; set; }

    public Product? Product { get; set; }
}
