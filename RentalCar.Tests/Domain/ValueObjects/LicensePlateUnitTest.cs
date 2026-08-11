using RentalCar.Domain.ValueObjects;
using FluentAssertions;
using Xunit;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCar.Tests.Domain.ValueObjects
{
    public class LicensePlateUnitTest
    {
        [Fact]
        public void LicensePlate_ShouldBeValid_WhenFormatIsCorrect()
        {
            // Arrange
            var validLicensePlate = "ABC1D23";
            // Act
            var licensePlate = new LicensePlate(validLicensePlate);
            // Assert
            licensePlate.Value.Should().Be(validLicensePlate);
        }

        [Fact]
        public void LicensePlate_ShouldThrowDomainException_WhenFormatIsIncorrect()
        {
            // Arrange
            var invalidLicensePlate = "INVALID";
            // Act
            Action act = () => new LicensePlate(invalidLicensePlate);
            //Assert
            act.Should().Throw<Exception>().WithMessage("License plate must be in the format 'ABC1234' or 'ABC1D23'.");
        }

        [Fact]
        public void LicensePlate_ShouldThrowDomainException_WhenValueIsNull()
        {
            // Arrange
            var invalidLicensePlate = null as string;
            // Act
            Action act = () => new LicensePlate(invalidLicensePlate);
            // Assert
            act.Should().Throw<Exception>().WithMessage("License plate cannot be null or whitespace.");
        }

        [Fact]
        public void LicensePlate_ShouldThrowDomainException_WhenValueIsEmpty()
        {
            // Arrange
            var invalidLicensePlate = "";
            // Act
            Action act = () => new LicensePlate(invalidLicensePlate);
            // Assert
            act.Should().Throw<Exception>().WithMessage("License plate cannot be null or whitespace.");
        }
    }
}
