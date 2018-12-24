using DMCore.Data.Core.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DMCore.Data.Persistance.EntityConfigurations
{
    internal class AuthRoleConfiguration : IEntityTypeConfiguration<AuthRole>
    {
        public void Configure(EntityTypeBuilder<AuthRole> builder)
        {
            builder.HasKey(x => x.Id);
            //builder.HasMany(u => u.AuthUsers).WithOne().HasForeignKey(ur => ur.Id).IsRequired();

        }
    }
}
