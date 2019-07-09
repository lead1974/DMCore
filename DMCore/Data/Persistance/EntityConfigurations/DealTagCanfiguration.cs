using DMCore.Data.Core.Domain;
using DMCore.Data.Core.Domain.Deal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DMCore.Data.Persistance.EntityConfigurations
{
    internal class DealTagConfiguration : IEntityTypeConfiguration<DealTag>
    {
        public void Configure(EntityTypeBuilder<DealTag> builder)
        {
            builder.ToTable("DealTags");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();

        }
    }
}
