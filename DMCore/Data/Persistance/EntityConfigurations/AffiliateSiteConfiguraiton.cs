using DMCore.Data.Core.Domain;
using DMCore.Data.Core.Domain.Deal;
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

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasMaxLength(255).IsRequired();
            builder.Property(x => x.Domain).HasMaxLength(255).IsRequired();

        }
    }
}
