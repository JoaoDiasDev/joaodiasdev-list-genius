using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ListGenius.Api.Context.Configurations
{
    public class ProductsListConfiguration : IEntityTypeConfiguration<ProductsList>
    {
        public void Configure(EntityTypeBuilder<ProductsList> builder)
        {
            builder.ToTable("products_lists");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.IdUser).IsRequired().HasMaxLength(100);
            builder.HasIndex(p => p.Name).IsUnique();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).HasMaxLength(200);
            builder.Property(p => p.Enabled).IsRequired().HasDefaultValue(true);
            builder.Property(p => p.Image).HasMaxLength(500);
            builder.Property(p => p.Public).IsRequired().HasDefaultValue(false);
            builder.Property(p => p.ExternalLink).HasMaxLength(500);
            builder.Property(p => p.CreatedDate).IsRequired().HasColumnType("datetime");
            builder.Property(p => p.UpdatedDate).IsRequired().HasColumnType("datetime");

            builder.HasOne(p => p.User).WithMany(u => u.ProductsLists).HasForeignKey(p => p.IdUser);
        }
    }
}
