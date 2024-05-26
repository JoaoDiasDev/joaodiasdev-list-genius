using ListGenius.Api.Context.Configurations;
using ListGenius.Api.Entities.ProductGroups;
using ListGenius.Api.Entities.Products;
using ListGenius.Api.Entities.Products.Enums;
using ListGenius.Api.Entities.ProductShareds;
using ListGenius.Api.Entities.ProductsLists;
using ListGenius.Api.Entities.ProductSubGroups;
using ListGenius.Api.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ListGenius.Api.Context;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    private readonly IServiceProvider _serviceProvider;

    public AppDbContext(DbContextOptions<AppDbContext> options, IServiceProvider serviceProvider) : base(options)
    {
        _serviceProvider = serviceProvider;
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<ProductsList> ProductsLists { get; set; }
    public DbSet<ProductShared> ProductsShared { get; set; }
    public DbSet<ProductGroup> ProductGroups { get; set; }
    public DbSet<ProductSubGroup> ProductSubGroups { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        if (environment == Environments.Development)
        {
            optionsBuilder.EnableDetailedErrors();
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        builder.ApplyConfiguration(new ProductConfiguration());
        builder.ApplyConfiguration(new ProductsListConfiguration());
        builder.ApplyConfiguration(new ProductSharedConfiguration());
        builder.ApplyConfiguration(new ProductGroupConfiguration());
        builder.ApplyConfiguration(new ProductSubGroupConfiguration());

        #region Generating Roles
        builder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole { Id = "2", Name = "User", NormalizedName = "USER" }
        );
        #endregion
        #region Generating Users
        builder.Entity<ApplicationUser>().HasData(
          new ApplicationUser
          {
              Id = "37846734-172e-4149-8cec-6f43d1eb3f60",
              UserName = "jmmatheus23@gmail.com",
              NormalizedUserName = "JMMATHEUS23@GMAIL.COM",
              Email = "jmmatheus23@gmail.com",
              NormalizedEmail = "JMMATHEUS23@GMAIL.COM",
              FullName = "JoaoDiasUser",
              EmailConfirmed = true,
              PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(new ApplicationUser(), "Matheus321*"),
          },
          new ApplicationUser
          {
              Id = "38846734-172e-4149-8cec-6f43d1eb3f60",
              UserName = "joaodiasworking@gmail.com",
              NormalizedUserName = "JOAODIASWORKING@GMAIL.COM",
              Email = "joaodiasworking@gmail.com",
              NormalizedEmail = "JOAODIASWORKING@GMAIL.COM",
              FullName = "JoaoDiasAdmin",
              EmailConfirmed = true,
              PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(new ApplicationUser(), "Matheus321*"),
          }
          );
        builder.Entity<IdentityUserRole<string>>().HasData(
          new IdentityUserRole<string> { UserId = "37846734-172e-4149-8cec-6f43d1eb3f60", RoleId = "2" },
          new IdentityUserRole<string> { UserId = "38846734-172e-4149-8cec-6f43d1eb3f60", RoleId = "1" }
          );
        #endregion
        #region Generating Product Groups
        builder.Entity<ProductGroup>().HasData(
            new
            {
                Id = 1,
                Name = "GERAL",
                Description = "GERAL",
                Enabled = true,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Image = new byte[1]
            }
        );
        #endregion
        #region Generating Product Sub Groups
        builder.Entity<ProductSubGroup>().HasData(
            new
            {
                Id = 1,
                Name = "GERAL",
                Description = "GERAL",
                Enabled = true,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Image = new byte[1],
                IdProductGroup = 1
            }
        );
        #endregion
        #region Generating ProductsLists
        builder.Entity<ProductsList>().HasData(
            new
            {
                Id = 1,
                Name = "Shopping List 1",
                Description = "Description for Shopping List 1",
                ExternalLink = "",
                Image = new byte[1],
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                IdUser = "37846734-172e-4149-8cec-6f43d1eb3f60",
            },
            new
            {
                Id = 2,
                Name = "Shopping List 2",
                Description = "Description for Shopping List 2",
                ExternalLink = "",
                Image = new byte[1],
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                IdUser = "37846734-172e-4149-8cec-6f43d1eb3f60",
            }
        );
        #endregion
        #region Generating Products Shared
        builder.Entity<ProductShared>()
            .HasData(
        new
        {
            Id = 1,
            Name = "Teste 1",
            Enabled = true,
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now,
            Value = 22.05M,
            Description = "TESTE 1",
            Qrcode = new byte[1],
            Image = new byte[1],
            Link = "",
            Unit = UnitsOfMeasurement.Meter,
            IdProductGroup = 1,
            IdProductSubGroup = 1,
        },
        new
        {
            Id = 2,
            Name = "Teste 2",
            Enabled = true,
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now,
            Value = 33.33M,
            Description = "TESTE 2",
            Qrcode = new byte[1],
            Image = new byte[1],
            Link = "",
            Unit = UnitsOfMeasurement.SquareMeter,
            IdProductGroup = 1,
            IdProductSubGroup = 1,
        },
        new
        {
            Id = 3,
            Name = "Teste 3",
            Enabled = false,
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now,
            Value = 42.33M,
            Description = "TESTE 3",
            Qrcode = new byte[1],
            Image = new byte[1],
            Link = "",
            Unit = UnitsOfMeasurement.Unspecified,
            IdProductGroup = 1,
            IdProductSubGroup = 1,
        },
        new
        {
            Id = 4,
            Name = "Teste 4",
            Enabled = true,
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now,
            Value = 77.77M,
            Description = "TESTE 4",
            Qrcode = new byte[1],
            Image = new byte[1],
            Link = "",
            Unit = UnitsOfMeasurement.CubicMeter,
            IdProductGroup = 1,
            IdProductSubGroup = 1,
        },
        new
        {
            Id = 5,
            Name = "Teste 5",
            Enabled = true,
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now,
            Value = 66.66M,
            Description = "TESTE 5",
            Qrcode = new byte[1],
            Image = new byte[1],
            Link = "",
            Unit = UnitsOfMeasurement.Unit,
            IdProductGroup = 1,
            IdProductSubGroup = 1,
        },
        new
        {
            Id = 6,
            Name = "Teste 6",
            Enabled = false,
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now,
            Value = 35.31M,
            Description = "TESTE 6",
            Qrcode = new byte[1],
            Image = new byte[1],
            Link = "",
            Unit = UnitsOfMeasurement.Unspecified,
            IdProductGroup = 1,
            IdProductSubGroup = 1,
        }
        );
        #endregion
        #region Generating Products
        builder.Entity<Product>().HasData(
        new
        {
            Id = 1,
            Name = "Teste 1",
            Enabled = true,
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now,
            Value = 22.05M,
            Description = "TESTE 1",
            Qrcode = new byte[1],
            Image = new byte[1],
            Link = "",
            Unit = UnitsOfMeasurement.Meter,
            IdProductsList = 1,
            IdProductGroup = 1,
            IdProductSubGroup = 1,
        },
        new
        {
            Id = 2,
            Name = "Teste 2",
            Enabled = true,
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now,
            Value = 33.33M,
            Description = "TESTE 2",
            Qrcode = new byte[1],
            Image = new byte[1],
            Link = "",
            Unit = UnitsOfMeasurement.SquareMeter,
            IdProductsList = 1,
            IdProductGroup = 1,
            IdProductSubGroup = 1,
        },
        new
        {
            Id = 3,
            Name = "Teste 3",
            Enabled = false,
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now,
            Value = 42.33M,
            Description = "TESTE 3",
            Qrcode = new byte[1],
            Image = new byte[1],
            Link = "",
            Unit = UnitsOfMeasurement.Unspecified,
            IdProductsList = 1,
            IdProductGroup = 1,
            IdProductSubGroup = 1,
        },
        new
        {
            Id = 4,
            Name = "Teste 4",
            Enabled = true,
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now,
            Value = 77.77M,
            Description = "TESTE 4",
            Qrcode = new byte[1],
            Image = new byte[1],
            Link = "",
            Unit = UnitsOfMeasurement.CubicMeter,
            IdProductsList = 2,
            IdProductGroup = 1,
            IdProductSubGroup = 1,
        },
        new
        {
            Id = 5,
            Name = "Teste 5",
            Enabled = true,
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now,
            Value = 66.66M,
            Description = "TESTE 5",
            Qrcode = new byte[1],
            Image = new byte[1],
            Link = "",
            Unit = UnitsOfMeasurement.Unit,
            IdProductsList = 2,
            IdProductGroup = 1,
            IdProductSubGroup = 1,
        },
        new
        {
            Id = 6,
            Name = "Teste 6",
            Enabled = false,
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now,
            Value = 35.31M,
            Description = "TESTE 6",
            Qrcode = new byte[1],
            Image = new byte[1],
            Link = "",
            Unit = UnitsOfMeasurement.Unspecified,
            IdProductsList = 2,
            IdProductGroup = 1,
            IdProductSubGroup = 1,
        }
        );
        #endregion

        foreach (var relationship in builder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

    }
}
