using RetailPulseAI.Domain.Common;

namespace RetailPulseAI.Domain.Entities;

public sealed class Inventory : BaseEntity
{
    public Guid StoreId { get; set; }
    public Guid ProductId { get; set; }
    public int OnHandUnits { get; set; }
    public DateOnly SnapshotDate { get; set; }

    public Store? Store { get; set; }
    public Product? Product { get; set; }
}
