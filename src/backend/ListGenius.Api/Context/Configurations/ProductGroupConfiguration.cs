using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ListGenius.Api.Context.Configurations;

public class ProductGroupConfiguration : IEntityTypeConfiguration<ProductGroup>
{
    public void Configure(EntityTypeBuilder<ProductGroup> builder)
    {
        builder.ToTable("product_groups");
        builder.HasKey(p => p.Id);
        builder.HasIndex(p => p.Name).IsUnique();
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Image).HasMaxLength(500);
        builder.Property(p => p.Description).HasMaxLength(200);
        builder.Property(p => p.Enabled).IsRequired();
        builder.Property(p => p.CreatedDate).IsRequired().HasColumnType("datetime");
        builder.Property(p => p.UpdatedDate).IsRequired().HasColumnType("datetime");
    }
}
