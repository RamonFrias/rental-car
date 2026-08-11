using FluentAssertions;
using RentalCar.Domain.ValueObjects;

namespace RentalCar.Tests.Domain.ValueObjects
{
    public class EmailUnitTest
    {
        [Fact]
        public void Email_ShouldBeValid()
        {
            // Arrange
            var email = "test@example.com";
            // Act
            var emailValueObject = new Email(email);
            // Assert
            emailValueObject.Value.Should().Be(email);
        }

        [Fact]
        public void Email_ShouldThrowException_WhenEmailIsInvalid()
        {
            // Arrange
            var invalidEmail = "testExemple.com.br";
            // Act
            Action act = () => new Email(invalidEmail);
            // Assert
            act.Should().Throw<Exception>().WithMessage("Email is not valid.");
        }

        [Fact]
        public void Email_ShouldThrowException_WhenEmailIsNullOrEmpty()
        {
            // Arrange
            var invalidEmail = "";
            // Act
            Action act = () => new Email(invalidEmail);
            // Assert
            act.Should().Throw<Exception>().WithMessage("Email cannot be null or empty.");
        }
    }
}
