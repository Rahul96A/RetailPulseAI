using RetailPulseAI.Domain.Common;

namespace RetailPulseAI.Domain.Entities;

public sealed class RetailSale : BaseEntity
{
    public Guid StoreId { get; set; }
    public Guid ProductId { get; set; }
    public DateOnly Date { get; set; }
    public int UnitsSold { get; set; }
    public decimal Revenue { get; set; }
    public decimal Price { get; set; }

    public Store? Store { get; set; }
    public Product? Product { get; set; }
}
