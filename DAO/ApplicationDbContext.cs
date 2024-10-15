﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Entities;

namespace DAO
{
    public class ApplicationDbContext(DbContextOptions options) :
        IdentityDbContext<Account, Role, string>(options)
    {
        public DbSet<FlowerListing> FlowerListings { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            const string priceDecimalType = "decimal(18,2)";
            const string tablePrefix = "BlossomApp";

            builder.Entity<Account>()
                .HasMany(u => u.Claims)
                .WithOne()
                .HasForeignKey(c => c.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Account>()
                .HasMany(u => u.Roles)
                .WithOne()
                .HasForeignKey(r => r.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Role>()
                .HasMany(r => r.Claims)
                .WithOne()
                .HasForeignKey(c => c.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Role>()
                .HasMany(r => r.Users)
                .WithOne()
                .HasForeignKey(r => r.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // Configure FlowerListing relationships
            builder.Entity<FlowerListing>(entity =>
            {
                entity.HasOne(f => f.Seller)
                    .WithMany()
                    .HasForeignKey(f => f.SellerId);

                entity.HasOne(f => f.Category)
                    .WithMany()
                    .HasForeignKey(f => f.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure Order relationships
            builder.Entity<Order>(entity =>
            {
                entity.HasOne(o => o.User)
                    .WithMany()
                    .HasForeignKey(o => o.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(o => o.OrderDetails)
                    .WithOne(od => od.Order)
                    .HasForeignKey(od => od.OrderId);
            });

            // Configure OrderDetail relationships
            builder.Entity<OrderDetail>(entity =>
            {
                entity.HasOne(od => od.Order)
                    .WithMany()
                    .HasForeignKey(od => od.OrderId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(od => od.Seller)
                    .WithMany()
                    .HasForeignKey(od => od.SellerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(od => od.FlowerListing)
                    .WithMany()
                    .HasForeignKey(od => od.FlowerListingId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure CartItem relationships
            builder.Entity<CartItem>(entity =>
            {
                entity.HasOne(ci => ci.User)
                    .WithMany()
                    .HasForeignKey(ci => ci.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
                

                entity.HasOne(ci => ci.FlowerListing)
                    .WithMany()
                    .HasForeignKey(ci => ci.FlowerListingId)
                    .OnDelete(DeleteBehavior.Restrict);
                
            });
        }

        public override int SaveChanges()
        {
            AddAuditInfo();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            AddAuditInfo();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddAuditInfo();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            AddAuditInfo();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void AddAuditInfo()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is IAuditableEntity &&
                           (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entry in modifiedEntries)
            {
                var entity = (IAuditableEntity)entry.Entity;
                var now = DateTime.UtcNow;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = now;
                }
                else
                {
                    base.Entry(entity).Property(x => x.CreatedAt).IsModified = false;
                }

                entity.UpdatedAt = now;
            }
        }
    } 
}