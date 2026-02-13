using RetailPulseAI.Domain.Interfaces;

namespace RetailPulseAI.Infrastructure.MultiTenancy;

public sealed class TenantContext : ITenantContext
{
    public Guid TenantId { get; private set; }
    public string TenantSlug { get; private set; } = string.Empty;
    public bool IsResolved { get; private set; }

    public void SetTenant(Guid tenantId, string tenantSlug)
    {
        TenantId = tenantId;
        TenantSlug = tenantSlug;
        IsResolved = true;
    }
}
