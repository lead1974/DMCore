using DMCore.Data.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DMCore.Data.Persistance.EntityConfigurations
{
    internal class AffiliateSiteConfiguration : IEntityTypeConfiguration<AffiliateSite>
    {
        public void Configure(EntityTypeBuilder<AffiliateSite> builder)
        {
            builder.ToTable("AffiliateSites");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasColumnName("Name").HasMaxLength(255).IsRequired();
            builder.Property(x => x.URL).HasColumnName("URL").HasMaxLength(255).IsRequired();

        }
    }
}
