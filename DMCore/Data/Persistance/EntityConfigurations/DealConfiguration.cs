using DMCore.Data.Core.Domain;
using DMCore.Data.Core.Domain.Deal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DMCore.Data.Persistance.EntityConfigurations
{
    internal class DealConfiguration : IEntityTypeConfiguration<Deal>
    {
        public void Configure(EntityTypeBuilder<Deal> builder)
        {
            builder.ToTable("Deals");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Instructions).IsRequired();
            builder.Property(x => x.URL).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(255).IsRequired();

            builder.HasOne(x => x.DealCategory).WithMany(b => b.Deals).HasForeignKey(b => b.DealCategoryId).OnDelete(DeleteBehavior.ClientSetNull);
            builder.HasOne(x => x.Store).WithMany(b => b.Deals).HasForeignKey(b => b.StoreId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.DealTag).WithMany(b => b.Deals).HasForeignKey(b => b.DealTagId).OnDelete(DeleteBehavior.ClientSetNull);
            builder.HasOne(x => x.AffiliateSite).WithMany(b => b.Deals).HasForeignKey(b => b.AffiliateSiteId).OnDelete(DeleteBehavior.ClientSetNull);

            //builder.Metadata.FindNavigation(nameof(Author.Blogs)).SetPropertyAccessMode(PropertyAccessMode.Field);

        }
    }
}
