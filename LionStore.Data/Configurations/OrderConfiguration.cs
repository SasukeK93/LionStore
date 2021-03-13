using LionStore.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace LionStore.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
                .HasKey(a => a.Id);

            builder
                .Property(m => m.Id);

            builder
                .Property(m => m.Timestamp)
                .IsRequired();

            builder
                .Property(m => m.Status)
                .IsRequired();

            builder
                .Property(m => m.Quantity)
                .IsRequired();

            builder
                .HasOne(m => m.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(m => m.LionUserId);

            builder
                .HasOne(m => m.Product) // Specified as single on the document
                .WithMany(p=>p.Orders)
                .HasForeignKey(m => m.ProductId);
        }
    }
}
