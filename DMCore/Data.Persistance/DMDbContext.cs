using DMCore.Data.Core.Domain;
using DMCore.Data.Persistance.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DMCore.Data.Persistance
{
    public class DMDbContext : DbContext
    {
        public DMDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Deal> Deals { get; protected set; }
        public virtual DbSet<DealCategory> DealCategories { get; protected set; }
        public virtual DbSet<Coupon> Coupons { get; protected set; }
        public virtual DbSet<Store> Stores { get; protected set; }
        public virtual DbSet<AuthUser> AuthUsers { get; protected set; }
        public virtual DbSet<AuthRole> AuthRoles { get; protected set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DealConfiguration());
            modelBuilder.ApplyConfiguration(new DealCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new DealTagConfiguration());
            modelBuilder.ApplyConfiguration(new CouponConfiguration());
            modelBuilder.ApplyConfiguration(new StoreConfiguration());
            modelBuilder.ApplyConfiguration(new AffiliateSiteConfiguration());           
        }
    }
}
