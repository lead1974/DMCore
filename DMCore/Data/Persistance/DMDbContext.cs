using DMCore.Data.Core.Domain;
using DMCore.Data.Core.Domain.Deal;
using DMCore.Data.Persistance.EntityConfigurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DMCore.Data.Persistance
{
    public class DMDbContext : IdentityDbContext<AuthUser, AuthRole, string>
    {
        public DMDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Deal> Deals { get; protected set; }
        public virtual DbSet<DealCategory> DealCategories { get; protected set; }
        public virtual DbSet<Coupon> Coupons { get; protected set; }
        public virtual DbSet<Store> Stores { get; protected set; }
        public virtual DbSet<DealTag> TagDeals { get; protected set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Make sure this line in place to avoid "dotnet ef database update" errors!!! 

            modelBuilder.ApplyConfiguration(new DealConfiguration());
            modelBuilder.ApplyConfiguration(new DealCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new DealTagConfiguration());
            modelBuilder.ApplyConfiguration(new CouponConfiguration());
            modelBuilder.ApplyConfiguration(new StoreConfiguration());
            modelBuilder.ApplyConfiguration(new AffiliateSiteConfiguration());
        }
    }
}
