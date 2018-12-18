using DMCore.Data.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DMCore.Data.Persistance.EntityConfigurations
{
    internal class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.ToTable("Stores");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("Id").ValueGeneratedOnAdd();            
            builder.Property(x => x.Name).HasColumnName("Name").HasMaxLength(255).IsRequired();
            builder.Property(x => x.StoreTips).HasColumnName("StoreTips").HasMaxLength(255).IsRequired();
            builder.Property(x => x.Instructions).HasColumnName("Instrunctions").IsRequired();

            builder.HasOne(x => x.DealCategory).WithMany(b => b.Stores).HasForeignKey(b => b.DealCategoryId).OnDelete(DeleteBehavior.SetNull);

        }
    }
}
