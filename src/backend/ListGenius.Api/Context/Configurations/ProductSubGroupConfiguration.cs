using ListGenius.Api.Entities.ProductSubGroups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ListGenius.Api.Context.Configurations
{
    public class ProductSubGroupConfiguration : IEntityTypeConfiguration<ProductSubGroup>
    {
        public void Configure(EntityTypeBuilder<ProductSubGroup> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Image).HasMaxLength(500);
            builder.Property(p => p.Description).HasMaxLength(200);
            builder.Property(p => p.Enabled).IsRequired().HasDefaultValue(true);
            builder.Property(p => p.CreatedDate).IsRequired().HasColumnType("datetime");
            builder.Property(p => p.UpdatedDate).IsRequired().HasColumnType("datetime");

            builder.HasOne(p => p.ProductGroup).WithMany(pg => pg.ProductSubGroups).HasForeignKey(p => p.IdProductGroup);
        }
    }
}
