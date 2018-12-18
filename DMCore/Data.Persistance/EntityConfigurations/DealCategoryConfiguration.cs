using DMCore.Data.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DMCore.Data.Persistance.EntityConfigurations
{
    internal class DealCategoryConfiguration : IEntityTypeConfiguration<DealCategory>
    {
        public void Configure(EntityTypeBuilder<DealCategory> builder)
        {
            builder.ToTable("DealCategories");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasColumnName("Name").HasMaxLength(255).IsRequired();
            builder.Property(x => x.Status).HasColumnName("Status").HasMaxLength(50);

        }
    }
}
