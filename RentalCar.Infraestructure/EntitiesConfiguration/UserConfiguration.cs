using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalCar.Domain.Entities;
using RentalCar.Domain.ValueObjects;

namespace RentalCar.Infraestructure.EntitiesConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).ValueGeneratedNever();

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(500)
                .HasColumnName("name");

            builder.Property(u => u.BirthDate)
               .IsRequired()
               .HasColumnName("birth_date");

            builder.Property(u => u.Email).HasConversion(email => email.Value,
                value => new Email(value))
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("email");
            builder.HasIndex(u => u.Email).IsUnique();

            builder.OwnsOne(u => u.Address, address =>
            {
                address.Property(a => a.Street)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("street");
                address.Property(a => a.City)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("city");
                address.Property(a => a.State)
                    .HasConversion(
                        state => state.Value,
                        value => new State(value))
                    .HasColumnName("State")
                    .HasMaxLength(2);

                address.Property(a => a.ZipCode)
                    .HasConversion(
                        zip => zip.Value,
                        value => new ZipCode(value))
                    .HasColumnName("ZipCode")
                    .HasMaxLength(8);
            });
        }
    }
}
