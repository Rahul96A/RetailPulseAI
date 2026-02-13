using RetailPulseAI.Domain.Common;

namespace RetailPulseAI.Domain.Entities;

public sealed class Store : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
}
