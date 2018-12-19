using DMCore.Data.Core.Domain;
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

            builder.Property(x => x.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(x => x.Instructions).HasColumnName("Instructions").IsRequired();
            builder.Property(x => x.URL).HasColumnName("URL").IsRequired();
            builder.Property(x => x.Price).HasColumnName("Price").IsRequired();
            builder.Property(x => x.Title).HasColumnName("Title").HasMaxLength(255).IsRequired();

            //builder.HasOne(x => x.DealCategory).WithMany(b => b.Deals).HasForeignKey(b => b.DealCategoryId).OnDelete(DeleteBehavior.Cascade);
            //builder.HasOne(x => x.DealTag).WithMany(b => b.Deals).HasForeignKey(b => b.DealTagId).OnDelete(DeleteBehavior.SetNull);

            //builder.Metadata.FindNavigation(nameof(Author.Blogs)).SetPropertyAccessMode(PropertyAccessMode.Field);

        }
    }
}
