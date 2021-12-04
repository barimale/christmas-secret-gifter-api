using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Albergue.Administrator.Repository.Database.Configuration
{
    public class IdentityUserEntryConfiguration : IEntityTypeConfiguration<IdentityUser>
    {
        public void Configure(EntityTypeBuilder<IdentityUser> builder)
        {
            var user = new IdentityUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "Admin",
                Email = "mateusz.wolnica@gmail.com",
                LockoutEnabled = false,
                PhoneNumber = "0048665337563",
                NormalizedUserName = "ADMIN",
                NormalizedEmail = "MATEUSZ.WOLNICA@GMAIL.COM",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = new Guid().ToString("D")
            };

            var passwordHasher = new PasswordHasher<IdentityUser>();
            user.PasswordHash = passwordHasher.HashPassword(user, "Admin?123");

            builder.HasData(user);
        }
    }
}
