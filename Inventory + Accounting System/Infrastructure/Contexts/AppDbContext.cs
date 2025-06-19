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

        public DbSet<StockTransactions> stockTransactions { get; set; }

        public DbSet<PurchaseInvoice> PurchaseInvoices { get; set; }
        public DbSet<PurchaseItems> PurchaseItems { get; set; }

        public DbSet<SalesInvoice> SalesInvoices { get; set; }

        public DbSet<SalesItems> SalesItems { get; set; }   


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
            modelBuilder.Entity<StockTransactions>()
                .HasOne(x => x.Stock).
                 WithMany(x =>x.stockTransactions)
                .HasForeignKey(x => x.StockId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Vendor>()
                .HasMany(x => x.PurchaseInvoices)
                .WithOne(x => x.Vendor)
                .HasForeignKey(x => x.VentorsId);
            modelBuilder.Entity<PurchaseInvoice>()
                .HasMany(x => x.purchaseItems)
                .WithOne(x => x.PurchaseInvoice)
                .HasForeignKey(x => x.PurchaseInvoiceId);
            modelBuilder.Entity<PurchaseItems>()
                .HasOne(x => x.product)
                .WithMany(z => z.PurchaseItems)
                .HasForeignKey(x => x.ProductId);
            modelBuilder.Entity<PurchaseItems>()
                .Property(x => x.UnitPrice)
                .HasPrecision(18, 2);
            modelBuilder.Entity<PurchaseItems>()
                .Property(x => x.GSTAmount)
                .HasPrecision(18, 2);
            modelBuilder.Entity<PurchaseItems>()
                .Property(x => x.ToTalAmount)
                .HasPrecision(18, 2);
            modelBuilder.Entity<PurchaseInvoice>()
                .Property(x => x.TotalAmount)
                .HasPrecision(18, 2);
            modelBuilder.Entity<PurchaseInvoice>()
                .Property(x => x.GrantToTal)
                .HasPrecision(18, 2);
            modelBuilder.Entity<PurchaseItems>()
                .Property(x => x.GSTPercentage)
                .HasPrecision(18, 2);
            modelBuilder.Entity<PurchaseInvoice>()
                .Property(x => x.GST)
                .HasPrecision(18, 2);
            modelBuilder.Entity<StockTransactions>()
                .Property(x => x.TransactionType)
                .HasConversion<string>();
            modelBuilder.Entity<SalesInvoice>()
                .HasOne(x => x.Customer)
                .WithMany()
                .HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SalesItems>()
                .HasOne(x => x.SalesInvoice)
                .WithMany(x => x.SalesItems)
                .HasForeignKey(x => x.SalesInvoiceId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SalesItems>()
                .HasOne(x => x.Product)
                .WithMany(x => x.SalesItems)
                .HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<SalesInvoice>()
                .Property(x => x.TotalAmount)
                .HasPrecision(18, 2);
            modelBuilder.Entity<SalesItems>()
                .Property(x => x.UNITPrice)
                .HasPrecision(18, 2);
            modelBuilder.Entity<SalesItems>()
                .Property(x => x.Discount)
                .HasPrecision(18, 2);
            modelBuilder.Entity<SalesItems>()
                .Property(x => x.Gst)
                 .HasPrecision(18, 2);
            modelBuilder.Entity<SalesItems>()
                 .Property(x => x.TotalPrice)
                 .HasPrecision(18, 2);
            modelBuilder.Entity<Product>()
                .Property(x => x.SalesGst)
                .HasPrecision(18, 2);
            modelBuilder.Entity<Product>()
               .Property(x => x.PurchaseGST)
               .HasPrecision(18, 2);




        }
    }
}
