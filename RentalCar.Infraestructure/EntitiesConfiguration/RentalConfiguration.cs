using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCar.Infraestructure.EntitiesConfiguration
{
    public class RentalConfiguration : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> builder)
        {
            builder.ToTable("rentals");

            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedNever();

            builder.Property(r => r.StartDate)
                .IsRequired()
                .HasColumnName("start_date")
                .HasColumnType("datetime");

            builder.Property(r => r.EndDate)
                .IsRequired()
                .HasColumnName("end_date")
                .HasColumnType("datetime");

            builder.Property(r => r.Discount)
                .HasPrecision(10, 2)
                .IsRequired()
                .HasColumnName("discount");

            builder.Property(r => r.TotalAmount)
                .HasPrecision(10, 2)
                .IsRequired()
                .HasColumnName("total_amount");

            builder.HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Car)
                .WithMany()
                .HasForeignKey(r => r.CarId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
