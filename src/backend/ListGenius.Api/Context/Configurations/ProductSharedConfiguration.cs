using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ListGenius.Api.Context.Configurations;
public class ProductSharedConfiguration : IEntityTypeConfiguration<ProductShared>
{
    public void Configure(EntityTypeBuilder<ProductShared> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Value).IsRequired().HasPrecision(10, 2).HasColumnType("decimal(10,2)");
        builder.Property(p => p.Description).HasMaxLength(200);
        builder.Property(p => p.Qrcode).HasMaxLength(500);
        builder.Property(p => p.Image).HasMaxLength(500);
        builder.Property(p => p.Link).HasMaxLength(500);
        builder.Property(p => p.Enabled).IsRequired().HasDefaultValue(true);
        builder.Property(p => p.CreatedDate).IsRequired().HasColumnType("datetime");
        builder.Property(p => p.UpdatedDate).IsRequired().HasColumnType("datetime");
        builder.Property(p => p.Unit).HasConversion<string>().IsRequired().HasMaxLength(20);

        builder.HasOne(p => p.ProductGroup).WithMany(pg => pg.ProductsShared).HasForeignKey(p => p.IdProductGroup);
        builder.HasOne(p => p.ProductSubGroup).WithMany(psg => psg.ProductsShared).HasForeignKey(p => p.IdProductSubGroup);
    }

}
