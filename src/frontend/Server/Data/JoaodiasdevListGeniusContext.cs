using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius;

namespace JoaoDiasDev.ListGenius.UI.Server.Data
{
    public partial class joaodiasdev_list_geniusContext : DbContext
    {
        public joaodiasdev_list_geniusContext()
        {
        }

        public joaodiasdev_list_geniusContext(DbContextOptions<joaodiasdev_list_geniusContext> options) : base(options)
        {
        }

        partial void OnModelBuilding(ModelBuilder builder);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product>()
              .HasOne(i => i.Group)
              .WithMany(i => i.Products)
              .HasForeignKey(i => i.id_groups)
              .HasPrincipalKey(i => i.id);

            builder.Entity<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product>()
              .HasOne(i => i.ProductsList)
              .WithMany(i => i.Products)
              .HasForeignKey(i => i.id_products_list)
              .HasPrincipalKey(i => i.id);

            builder.Entity<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product>()
              .HasOne(i => i.SubGroup)
              .WithMany(i => i.Products)
              .HasForeignKey(i => i.id_sub_groups)
              .HasPrincipalKey(i => i.id);

            builder.Entity<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList>()
              .HasOne(i => i.User)
              .WithMany(i => i.ProductsLists)
              .HasForeignKey(i => i.id_users)
              .HasPrincipalKey(i => i.id);

            builder.Entity<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct>()
              .HasOne(i => i.Group)
              .WithMany(i => i.SharedProducts)
              .HasForeignKey(i => i.id_groups)
              .HasPrincipalKey(i => i.id);

            builder.Entity<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct>()
              .HasOne(i => i.SubGroup)
              .WithMany(i => i.SharedProducts)
              .HasForeignKey(i => i.id_sub_groups)
              .HasPrincipalKey(i => i.id);

            builder.Entity<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup>()
              .HasOne(i => i.Group)
              .WithMany(i => i.SubGroups)
              .HasForeignKey(i => i.id_groups)
              .HasPrincipalKey(i => i.id);

            builder.Entity<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group>()
              .Property(p => p.enabled)
              .HasDefaultValueSql(@"'1'");     

            builder.Entity<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product>()
              .Property(p => p.enabled)
              .HasDefaultValueSql(@"'1'");     

            builder.Entity<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList>()
              .Property(p => p.enabled)
              .HasDefaultValueSql(@"'1'");     

            builder.Entity<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct>()
              .Property(p => p.enabled)
              .HasDefaultValueSql(@"'1'");     

            builder.Entity<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup>()
              .Property(p => p.enabled)
              .HasDefaultValueSql(@"'1'");     

            builder.Entity<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User>()
              .Property(p => p.user_name)
              .HasDefaultValueSql(@"'0'");     

            builder.Entity<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User>()
              .Property(p => p.password)
              .HasDefaultValueSql(@"'0'");     

            builder.Entity<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User>()
              .Property(p => p.email)
              .HasDefaultValueSql(@"'email@email.com'");     

            builder.Entity<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User>()
              .Property(p => p.enabled)
              .HasDefaultValueSql(@"'1'");     

            builder.Entity<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User>()
              .Property(p => p.refresh_token)
              .HasDefaultValueSql(@"'0'");     

            builder.Entity<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group>()
              .Property(p => p.created_date)
              .HasColumnType("datetime(6)");

            builder.Entity<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group>()
              .Property(p => p.updated_date)
              .HasColumnType("datetime(6)");

            builder.Entity<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product>()
              .Property(p => p.created_date)
              .HasColumnType("datetime(6)");

            builder.Entity<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product>()
              .Property(p => p.updated_date)
              .HasColumnType("datetime(6)");

            builder.Entity<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList>()
              .Property(p => p.created_date)
              .HasColumnType("datetime(6)");

            builder.Entity<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList>()
              .Property(p => p.updated_date)
              .HasColumnType("datetime(6)");

            builder.Entity<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct>()
              .Property(p => p.created_date)
              .HasColumnType("datetime(6)");

            builder.Entity<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct>()
              .Property(p => p.updated_date)
              .HasColumnType("datetime(6)");

            builder.Entity<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup>()
              .Property(p => p.created_date)
              .HasColumnType("datetime(6)");

            builder.Entity<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup>()
              .Property(p => p.updated_date)
              .HasColumnType("datetime(6)");

            builder.Entity<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User>()
              .Property(p => p.refresh_token_expiry_time)
              .HasColumnType("datetime");
            this.OnModelBuilding(builder);
        }

        public DbSet<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Group> Groups { get; set; }

        public DbSet<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.Product> Products { get; set; }

        public DbSet<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.ProductsList> ProductsLists { get; set; }

        public DbSet<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SharedProduct> SharedProducts { get; set; }

        public DbSet<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.SubGroup> SubGroups { get; set; }

        public DbSet<JoaoDiasDev.ListGenius.UI.Server.Models.joaodiasdev_list_genius.User> Users { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Conventions.Add(_ => new BlankTriggerAddingConvention());
        }
    
    }
}