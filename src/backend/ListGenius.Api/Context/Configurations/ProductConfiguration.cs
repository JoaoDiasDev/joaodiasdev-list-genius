using ListGenius.Api.Entities.Products.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ListGenius.Api.Context.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
        builder.HasIndex(p => p.Name).IsUnique();
        builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
        builder.Property(p => p.Value).HasPrecision(10, 2).HasColumnType("decimal(10,2)").IsRequired();
        builder.Property(p => p.Description).HasMaxLength(200).IsRequired();
        builder.Property(p => p.Qrcode).HasMaxLength(500);
        builder.Property(p => p.Image).HasMaxLength(500);
        builder.Property(p => p.Link).HasMaxLength(500);
        builder.Property(p => p.Enabled).HasDefaultValue(true).IsRequired();
        builder.Property(p => p.CreatedDate).IsRequired().HasColumnType("datetime");
        builder.Property(p => p.UpdatedDate).IsRequired().HasColumnType("datetime");
        builder.Property(p => p.Unit)
            .HasConversion(p => p.ToString(),
            p => (UnitsOfMeasurement)Enum.Parse(typeof(UnitsOfMeasurement), p))
            .HasMaxLength(30)
            .IsRequired();

        builder.HasOne(p => p.ProductGroup).WithMany(pg => pg.Products).HasForeignKey(p => p.IdProductGroup);
        builder.HasOne(p => p.ProductSubGroup).WithMany(psg => psg.Products).HasForeignKey(p => p.IdProductSubGroup);
        builder.HasOne(p => p.ProductsList).WithMany(pl => pl.Products).HasForeignKey(p => p.IdProductsList);
    }
}
