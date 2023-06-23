using Microsoft.EntityFrameworkCore;
using P03_SalesDatabase.Data.Configuration;
using P03_SalesDatabase.Data.Models;

namespace P03_SalesDatabase.Data
{
    public class SalesContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Sale> Sales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-LBAOKKU\\SQLEXPRESS;Initial Catalog=SalesDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder
                .Entity<Customer>()
                .HasMany(v => v.Sales)
                .WithOne(p => p.Customer)
                .HasForeignKey(p => p.CustomerId)
                .HasPrincipalKey(p => p.CustomerId);

            modelBuilder.Entity<Customer>()
               .Property(p => p.Name)
               .IsUnicode(true)
               .HasMaxLength(100);

            modelBuilder.Entity<Customer>()
               .Property(p => p.Email)
               .IsUnicode(false)
               .HasMaxLength(80);

            modelBuilder
                 .Entity<Product>()
                 .HasMany(v => v.Sales)
                 .WithOne(p => p.Product)
                 .HasForeignKey(p => p.ProductId)
                 .HasPrincipalKey(p => p.ProductId);

            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .IsUnicode(true)
                .HasMaxLength(50);

            modelBuilder.Entity<Product>()
                .Property(p => p.Description)
                .HasMaxLength(250)
                .HasDefaultValue("No description");

            modelBuilder
                .Entity<Store>()
                .HasMany(v => v.Sales)
                .WithOne(p => p.Store)
                .HasForeignKey(p => p.StoreId)
                .HasPrincipalKey(p => p.StoreId);

            modelBuilder.Entity<Store>()
             .Property(p => p.Name)
             .IsUnicode(true)
             .HasMaxLength(80);

            modelBuilder.Entity<Sale>()
                .Property(p => p.Date)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.ApplyConfiguration(new SaleConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new StoreConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        }
    }
}
