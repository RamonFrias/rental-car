using FluentAssertions;
using RentalCar.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCar.Tests.Domain.ValueObjects
{
    public class AddressUnitTest
    {
        [Fact]
        public void Address_ShouldBeValid_WhenAllFieldsAreCorrect()
        {
            // Arrange
            var validStreet = "123 Main St";
            var validCity = "Anytown";
            var validNumber = 123;
            State validState = new State("SP");
            ZipCode validZipCode = new ZipCode("13321-382");
            // Act
            var address = new Address(validStreet, validNumber, validCity, validZipCode, validState);
            // Assert
            address.Should().Be(address);
        }

        [Fact]
        public void Address_shouldThrowDomainException_WhenZipCodeIsNullOrEmpty()
        {
            // Arrange
            var validStreet = "123 Main St";
            var validCity = "Anytown";
            var validNumber = 123;
            State validState = new State("SP");
            ZipCode validZipCode = new ZipCode("13321-382");
            // Act
            Action action = () => new Address(validStreet, validNumber, validCity, null, validState);
            // Assert
            action.Should().Throw<Exception>().WithMessage("Zip code cannot be null.");
        }

        [Fact]
        public void Address_shouldThrowDomainException_WhenStateIsNullOrEmpty()
        {
            // Arrange
            var validStreet = "123 Main St";
            var validCity = "Anytown";
            var validNumber = 123;
            State validState = new State("SP");
            ZipCode validZipCode = new ZipCode("13321-382");
            // Act
            Action action = () => new Address(validStreet, validNumber, validCity, validZipCode, null);
            // Assert
            action.Should().Throw<Exception>().WithMessage("State cannot be null.");
        }

        [Fact]
        public void Address_shouldThrowDomainException_WhenStreetIsNullOrEmpty()
        {
            // Arrange
            var validStreet = "";
            var validCity = "Anytown";
            var validNumber = 123;
            State validState = new State("SP");
            ZipCode validZipCode = new ZipCode("13321-382");
            // Act
            Action action = () => new Address(validStreet, validNumber, validCity, validZipCode, validState);
            // Assert
            action.Should().Throw<Exception>().WithMessage("Street cannot be null or empty.");
        }


        [Fact]
        public void Address_shouldThrowDomainException_WhenCityIsNullOrEmpty()
        {
            // Arrange
            var validStreet = "123 Main St";
            var validCity = "";
            var validNumber = 123;
            State validState = new State("SP");
            ZipCode validZipCode = new ZipCode("13321-382");
            // Act
            Action action = () => new Address(validStreet, validNumber, validCity, validZipCode, validState);
            // Assert
            action.Should().Throw<Exception>().WithMessage("City cannot be null or empty.");
        }

        [Fact]
        public void Address_shouldThrowDomainException_WhenNumberIsLessThanOrEqualToZero()
        {
            // Arrange
            var validStreet = "123 Main St";
            var validCity = "Anytown";
            var validNumber = 0;
            State validState = new State("SP");
            ZipCode validZipCode = new ZipCode("13321-382");
            // Act
            Action action = () => new Address(validStreet, validNumber, validCity, validZipCode, validState);
            // Assert
            action.Should().Throw<Exception>().WithMessage("Number must be greater than zero.");
        }

    }
}
