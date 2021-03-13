using LionStore.Core.Models;
using LionStore.Core.Models.Identity;
using LionStore.Data.Configurations;
using LionStore.Data.Configurations.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LionStore.Data
{
    
    public class LionStoreDbContext : IdentityDbContext<LionUser, IdentityRole, string>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        public LionStoreDbContext() { }
        public LionStoreDbContext(DbContextOptions<LionStoreDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Apply Identity Seed Configurations
            //builder.ApplyConfiguration(new LionUserConfiguration());
            //builder.ApplyConfiguration(new RoleConfiguration());
            //builder.ApplyConfiguration(new UserRoleConfiguration());

            // Apply Configurations
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new OrderConfiguration());
        }

        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<LionStoreDbContext>
        {
            public LionStoreDbContext CreateDbContext(string[] args)
            {
                var builder = new DbContextOptionsBuilder<LionStoreDbContext>();
                var connectionString = "server=127.0.0.1;database=lion_store;user=lion;password=lion;Allow Zero Datetime=true;Convert Zero Datetime=true";
                builder.UseMySql(connectionString,
                        mySqlOptionsAction: sqlOptions =>
                        {
                            sqlOptions.EnableRetryOnFailure(
                                maxRetryCount: 3,
                                maxRetryDelay: TimeSpan.FromSeconds(2),
                                errorNumbersToAdd: null);
                            sqlOptions.MigrationsAssembly("LionStore.Data");
                        });
                return new LionStoreDbContext(builder.Options);
            }
        }
    }
}
