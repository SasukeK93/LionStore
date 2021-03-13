using LionStore.Core.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace LionStore.Data.Configurations.Identity
{
    public class LionUserConfiguration : IEntityTypeConfiguration<LionUser>
    {
        public void Configure(EntityTypeBuilder<LionUser> builder)
        {
            var password = "Pass123$";

            var admin = new LionUser
            {
                Id = "1",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@lionstore.com",
                NormalizedEmail = "admin@lionstore.com".ToUpper(),
                EmailConfirmed = true,
            };
            admin.PasswordHash = new PasswordHasher<LionUser>().HashPassword(admin, password);

            var seller = new LionUser
            {
                Id = "2",
                UserName = "seller",
                NormalizedUserName = "SELLER",
                Email = "seller@lionstore.com",
                NormalizedEmail = "seller@lionstore.com".ToUpper(),
                EmailConfirmed = true
            };
            seller.PasswordHash = new PasswordHasher<LionUser>().HashPassword(seller, password);

            var client = new LionUser
            {
                Id = "3",
                UserName = "client",
                NormalizedUserName = "CLIENT",
                Email = "client@lionstore.com",
                NormalizedEmail = "client@lionstore.com".ToUpper(),
                EmailConfirmed = true
            };
            client.PasswordHash = new PasswordHasher<LionUser>().HashPassword(client, password);

            builder.HasData(admin, seller, client);
        }
    }
}
