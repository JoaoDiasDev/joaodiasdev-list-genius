using ListGenius.Api.Context.Configurations;
using ListGenius.Api.Entities.Products;
using ListGenius.Api.Entities.ProductShareds;
using ListGenius.Api.Entities.ProductsLists;
using ListGenius.Api.Entities.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ListGenius.Api.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductsList> ProductsLists { get; set; }
    public DbSet<ProductShared> ProductsShared { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        builder.ApplyConfiguration(new ProductConfiguration());
        builder.ApplyConfiguration(new ProductGroupConfiguration());
        builder.ApplyConfiguration(new ProductSharedConfiguration());
        builder.ApplyConfiguration(new ProductsListConfiguration());
        builder.ApplyConfiguration(new ProductSubGroupConfiguration());

        foreach (var relationship in builder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

    }
}
