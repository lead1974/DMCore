using DMCore.Data.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DMCore.Data.Persistance.EntityConfigurations
{
    internal class CouponConfiguration : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> builder)
        {
            builder.ToTable("Coupons");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Title).HasMaxLength(255).IsRequired();
            builder.Property(x => x.Code).HasMaxLength(255).IsRequired();

            //builder.HasOne(x => x.DealCategory).WithMany(b => b.Coupons).HasForeignKey(b => b.DealCategoryId).OnDelete(DeleteBehavior.Cascade);


        }
    }
}
