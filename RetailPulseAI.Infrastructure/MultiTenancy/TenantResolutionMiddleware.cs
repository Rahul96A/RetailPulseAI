using RetailPulseAI.Domain.Interfaces;
using System.Security.Claims;

namespace RetailPulseAI.Infrastructure.MultiTenancy;

public sealed class TenantResolutionMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context, ITenantContext tenantContext)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var tenantIdRaw = context.User.FindFirstValue("tenant_id");
            var tenantSlug = context.User.FindFirstValue("tenant_slug") ?? string.Empty;

            if (Guid.TryParse(tenantIdRaw, out var tenantId))
            {
                tenantContext.SetTenant(tenantId, tenantSlug);
            }
        }

        await next(context);
    }
}
