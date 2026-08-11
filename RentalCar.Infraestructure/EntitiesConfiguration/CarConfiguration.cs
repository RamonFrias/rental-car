using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalCar.Domain.Entities;
using RentalCar.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCar.Infraestructure.EntitiesConfiguration
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.ToTable("cars");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedNever();

            builder.Property(c => c.Model)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("model");

            builder.Property(c => c.LicensePlate)
                .HasConversion(
                    lp => lp.Value,
                    value => new LicensePlate(value))
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("license_plate");
            builder.HasIndex(c => c.LicensePlate)
                .IsUnique();

            builder.Property(c => c.DailyRate)
                .HasConversion(
                    dr => dr.Value,
                    value => new DailyRate(value))
                .HasPrecision(10,2)
                .IsRequired()
                .HasColumnName("daily_rate");

            builder.Property(c => c.IsAvailable)
                .IsRequired()
                .HasColumnName("is_available");
        }
    }
}
