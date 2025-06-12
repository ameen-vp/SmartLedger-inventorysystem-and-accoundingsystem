using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.Contexts
{
  public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

       public DbSet<User> Users { get; set; }

       public DbSet<Category> categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Costomer> Costomer { get; set; }

        public DbSet<Vendor> Vendors { get; set; }

        public DbSet<Stocks> Stocks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("Users");

            modelBuilder.Entity<Product>()
            .HasOne(x => x.category)
            .WithMany(x => x.Product)
            .HasForeignKey(x => x.CategoryId);
            
            modelBuilder.Entity<Product>()
                .Property(x => x.SellingPrice)
                .HasPrecision(18, 2);
            modelBuilder.Entity<Product>()
               .Property(x => x.PurchasePrice)
               .HasPrecision(18, 2);
            modelBuilder.Entity<Stocks>()
                .HasOne(x => x.product)
                .WithOne(x => x.Stocks)
                .HasForeignKey<Stocks>(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
