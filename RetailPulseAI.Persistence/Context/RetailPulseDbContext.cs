using Microsoft.EntityFrameworkCore;
using RetailPulseAI.Domain.Entities;
using RetailPulseAI.Domain.Interfaces;
using System.Reflection;

namespace RetailPulseAI.Persistence.Context;

public sealed class RetailPulseDbContext(DbContextOptions<RetailPulseDbContext> options, ITenantContext tenantContext) : DbContext(options)
{
    public static readonly Guid DemoTenantId = Guid.Parse("11111111-1111-1111-1111-111111111111");

    public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Store> Stores => Set<Store>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<RetailSale> RetailSales => Set<RetailSale>();
    public DbSet<CompetitorPrice> CompetitorPrices => Set<CompetitorPrice>();
    public DbSet<Promotion> Promotions => Set<Promotion>();
    public DbSet<Inventory> Inventories => Set<Inventory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<User>().HasQueryFilter(x => !tenantContext.IsResolved || x.TenantId == tenantContext.TenantId);
        modelBuilder.Entity<Store>().HasQueryFilter(x => !tenantContext.IsResolved || x.TenantId == tenantContext.TenantId);
        modelBuilder.Entity<Product>().HasQueryFilter(x => !tenantContext.IsResolved || x.TenantId == tenantContext.TenantId);
        modelBuilder.Entity<RetailSale>().HasQueryFilter(x => !tenantContext.IsResolved || x.TenantId == tenantContext.TenantId);
        modelBuilder.Entity<CompetitorPrice>().HasQueryFilter(x => !tenantContext.IsResolved || x.TenantId == tenantContext.TenantId);
        modelBuilder.Entity<Promotion>().HasQueryFilter(x => !tenantContext.IsResolved || x.TenantId == tenantContext.TenantId);
        modelBuilder.Entity<Inventory>().HasQueryFilter(x => !tenantContext.IsResolved || x.TenantId == tenantContext.TenantId);

        Seed(modelBuilder);
    }

    private static void Seed(ModelBuilder modelBuilder)
    {
        var storeId = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var productId = Guid.Parse("33333333-3333-3333-3333-333333333333");

        modelBuilder.Entity<Tenant>().HasData(new Tenant
        {
            Id = DemoTenantId,
            TenantId = DemoTenantId,
            Name = "Demo Retail Group",
            Slug = "demo-retail",
            ConnectionString = string.Empty,
            IsActive = true,
            CreatedAtUtc = DateTime.UtcNow
        });

        modelBuilder.Entity<Store>().HasData(new Store
        {
            Id = storeId,
            TenantId = DemoTenantId,
            Name = "Downtown Store",
            Region = "North"
        });

        modelBuilder.Entity<Product>().HasData(new Product
        {
            Id = productId,
            TenantId = DemoTenantId,
            Sku = "SKU-1001",
            Name = "RetailPulse Cola 500ml",
            Brand = "RetailPulse",
            Category = "Beverages",
            Cost = 1.2m
        });

        modelBuilder.Entity<RetailSale>().HasData(new RetailSale
        {
            Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
            TenantId = DemoTenantId,
            StoreId = storeId,
            ProductId = productId,
            Date = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-7)),
            UnitsSold = 120,
            Revenue = 300,
            Price = 2.5m
        });
    }
}
