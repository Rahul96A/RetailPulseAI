using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetailPulseAI.Domain.Entities;

namespace RetailPulseAI.Persistence.Configurations;

public sealed class TenantConfiguration : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.ToTable("Tenants");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
        builder.Property(x => x.Slug).HasMaxLength(150).IsRequired();
        builder.HasIndex(x => x.Slug).IsUnique();
    }
}

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Email).HasMaxLength(320).IsRequired();
        builder.HasIndex(x => new { x.TenantId, x.Email }).IsUnique();
    }
}

public sealed class StoreConfiguration : IEntityTypeConfiguration<Store>
{
    public void Configure(EntityTypeBuilder<Store> builder)
    {
        builder.ToTable("Stores");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
    }
}

public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Sku).HasMaxLength(50).IsRequired();
        builder.HasIndex(x => new { x.TenantId, x.Sku }).IsUnique();
    }
}

public sealed class RetailSaleConfiguration : IEntityTypeConfiguration<RetailSale>
{
    public void Configure(EntityTypeBuilder<RetailSale> builder)
    {
        builder.ToTable("RetailSales");
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Store).WithMany().HasForeignKey(x => x.StoreId);
        builder.HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId);

        builder.HasIndex(x => new { x.TenantId, x.Date });
        builder.HasIndex(x => new { x.ProductId, x.Date });
        builder.HasIndex(x => new { x.StoreId, x.Date });
    }
}

public sealed class CompetitorPriceConfiguration : IEntityTypeConfiguration<CompetitorPrice>
{
    public void Configure(EntityTypeBuilder<CompetitorPrice> builder)
    {
        builder.ToTable("CompetitorPrices");
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId);
        builder.HasIndex(x => new { x.TenantId, x.Date });
    }
}

public sealed class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
{
    public void Configure(EntityTypeBuilder<Promotion> builder)
    {
        builder.ToTable("Promotions");
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId);
    }
}

public sealed class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
{
    public void Configure(EntityTypeBuilder<Inventory> builder)
    {
        builder.ToTable("Inventories");
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Store).WithMany().HasForeignKey(x => x.StoreId);
        builder.HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId);
    }
}
