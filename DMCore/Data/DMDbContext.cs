using System;
using System.Collections.Generic;
using System.Text;
using DMCore.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DMCore.Data
{
    public class DMDbContext : IdentityDbContext<AuthUser, AuthRole, string>
    {
        public DbSet<Deal> Deals { get; set; }
        public DbSet<DealCategory> DealCategories { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<CouponCategory> CouponCategories { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<StoreCategory> StoreCategories { get; set; }
        public DMDbContext(DbContextOptions<DMDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AuthUser>(b => {
                b.HasMany(x => x.Roles).WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
            });

            modelBuilder.Entity<Deal>(entity =>
            {
                entity.Property(d => d.Instructions)
                                   .HasDefaultValue(string.Empty);
                entity.Property(d => d.DMProduct)
                                   .HasDefaultValue(false);
                entity.Property(d => d.Approved)
                                   .HasDefaultValue(false);
                entity.Property(d => d.Status)
                                   .HasDefaultValue(0);
                entity.Property(d => d.Views)
                                   .HasDefaultValue(0);
                entity.Property(d => d.Likes)
                                   .HasDefaultValue(0);
                entity.Property(d => d.Dislikes)
                                   .HasDefaultValue(0);
                entity.Property(d => d.StartTS)
                                   .HasDefaultValue(DateTime.Now);
                entity.Property(d => d.EndTS)
                                   .HasDefaultValue(DateTime.Now.AddDays(10));
                entity.Property(d => d.CreatedTS)
                                   .HasDefaultValue(DateTime.Now);
                entity.Property(d => d.UpdatedTS)
                                   .HasDefaultValue(DateTime.Now);
            });

            modelBuilder.Entity<Coupon>(entity =>
            {
                entity.Property(c => c.Approved)
                                   .HasDefaultValue(false);
                entity.Property(c => c.Status)
                                   .HasDefaultValue(0);
                entity.Property(c => c.Views)
                                   .HasDefaultValue(0);
                entity.Property(c => c.Likes)
                                   .HasDefaultValue(0);
                entity.Property(c => c.Dislikes)
                                   .HasDefaultValue(0);
                entity.Property(c => c.StartTS)
                                   .HasDefaultValue(DateTime.Now);
                entity.Property(c => c.EndTS)
                                   .HasDefaultValue(DateTime.Now.AddDays(10));
                entity.Property(c => c.CreatedTS)
                                   .HasDefaultValue(DateTime.Now);
                entity.Property(c => c.UpdatedTS)
                                   .HasDefaultValue(DateTime.Now);
            });

        }
    }
}
