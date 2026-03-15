using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RetailNetworkConsole2.Models;

namespace RetailNetworkConsole2.Data
{
    internal class RetailContext : DbContext
    {
        public RetailContext(DbContextOptions<RetailContext> options)
            : base(options) { }

        public DbSet<Seller> Sellers { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<ShopSeller> ShopSellers { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Review> Reviews { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ContactInfo>()
                .HasKey(c => c.SellerID);

            modelBuilder.Entity<Seller>()
                .HasOne(s=> s.ContactInfo)
                .WithOne(c=> c.Seller)
                .HasForeignKey<ContactInfo>(c=> c.SellerID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ShopSeller>()
                .HasKey(ss => new { ss.ShopID, ss.SellerID });

            modelBuilder.Entity<ShopSeller>()
                .HasOne(ss=> ss.Shop)
                .WithMany(s=> s.ShopSellers)
                .HasForeignKey(ss=> ss.ShopID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ShopSeller>()
                .HasOne(ss => ss.Seller)
                .WithMany(s => s.ShopSellers)
                .HasForeignKey(ss => ss.SellerID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Receipt>()
                .Property(r => r.ReceiptID)
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Review>()
                .Property(r => r.ReviewID)
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Receipt>()
                .HasIndex(r => r.ReceiptNumber)
                .IsUnique();

            modelBuilder.Entity<Review>()
                .ToTable(t => t.HasCheckConstraint("CK_Review_Rating", "Rating BETWEEN 1 AND 5"));
        }
    }
}
