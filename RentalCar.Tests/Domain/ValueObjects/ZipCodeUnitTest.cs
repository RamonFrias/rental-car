using FluentAssertions;
using RentalCar.Domain.ValueObjects;

namespace RentalCar.Tests.Domain.ValueObjects
{
    public class ZipCodeUnitTest
    {
        [Fact]
        public void ZipCode_ShouldBeValid_WhenGivenValidZipCode()
        {
            // Arrange
            var validZipCode = "13321-123";
            // Act
            var zipCode = new ZipCode(validZipCode);
            // Assert
            zipCode.Value.Should().Be(validZipCode);
        }

        [Fact]
        public void ZipCode_ShouldThrowDomainException_WhenGivenInvalidZipCode()
        {
            // Arrange
            var invalidZipCode = "1234"; // Invalid zip code (less than 5 digits)
            // Act
            Action act = () => new ZipCode(invalidZipCode);
            // Assert
            act.Should().Throw<Exception>().WithMessage("Zip code must be in the format 12345-678.");
        }

        [Fact]
        public void ZipCode_ShouldThrowDomainException_WhenGivenEmptyZipCode()
        {
            // Arrange
            string emptyZipCode = "";
            // Act
            Action actEmpty = () => new ZipCode(emptyZipCode);
            // Assert
            actEmpty.Should().Throw<Exception>().WithMessage("Zip code cannot be null or empty.");
        }

        [Fact]
        public void ZipCode_ShouldThrowDomainException_WhenGivenNullZipCode()
        {
            // Arrange
            string nullZipCode = null;
            // Act
            Action actNull = () => new ZipCode(nullZipCode);
            // Assert
            actNull.Should().Throw<Exception>().WithMessage("Zip code cannot be null or empty.");
        }
    }
}