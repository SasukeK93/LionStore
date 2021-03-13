using LionStore.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace LionStore.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .HasKey(a => a.Id);

            builder
                .Property(m => m.Id);

            builder
                .Property(m => m.Name)
                .IsRequired();

            builder
                .Property(m => m.Description)
                .HasMaxLength(100);
            // Required ?

            builder
                .Property(m => m.Quantity)
                .IsRequired();

            builder
                .Property(m => m.Slug)
                .IsRequired();

            builder
                .Property(m => m.Price)
                .IsRequired();

            builder
                .HasOne(m => m.Owner)
                .WithMany(u => u.Products)
                .HasForeignKey(m => m.LionUserId);

            builder
                .ToTable("Products");
        }
    }
}
