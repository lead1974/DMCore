using DMCore.Data.Core.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DMCore.Data.Persistance.EntityConfigurations
{
    internal class AuthUserConfiguration : IEntityTypeConfiguration<AuthUser>
    {
        public void Configure(EntityTypeBuilder<AuthUser> builder)
        {
            builder.HasKey(x => x.Id);
            //builder.HasMany(u => u.AuthRoles).WithOne().HasForeignKey(ur => ur.Id).IsRequired();

        }
    }
}
