using FluentAssertions;
using RentalCar.Domain.Entities;
using RentalCar.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCar.Tests.Domain.Entities
{
    public class CarUnitTest
    {
        [Fact]
        public void CreateCar_WithValidParameters()
        {
            //Arrange
            var model = "Toyota Corolla";
            LicensePlate licensePlate = new LicensePlate("ABC1D23");
            DailyRate dailyRate = new DailyRate(50.0m);
            //Act
            var car = new Car(model, licensePlate, dailyRate);
            //Assert
            car.Should().Be(car);
        }

        [Fact]
        public void CreateCar_shouldBeThrowDomainException_WhenModelIsNullOrEmpty()
        {
            //Arrange
            var model = string.Empty;
            LicensePlate licensePlate = new LicensePlate("ABC1D23");
            DailyRate dailyRate = new DailyRate(50.0m);
            //Act
            Action act = () => new Car(model, licensePlate, dailyRate);
            //Assert
            act.Should().Throw<Exception>().WithMessage("Model cannot be null or empty.");
        }

        [Fact]
        public void CreateCar_shouldBeThrowDomainException_WhenLicensePlateIsNullOrEmpty()
        {
            //Arrange
            var model = "Toyota Corolla";
            LicensePlate licensePlate = null;
            DailyRate dailyRate = new DailyRate(50.0m);
            //Act
            Action act = () => new Car(model, licensePlate, dailyRate);
            //Assert
            act.Should().Throw<Exception>().WithMessage("License plate cannot be null.");
        }

        [Fact]
        public void CreateCar_shouldBeThrowDomainException_WhenDailyRateIsNullOrEmpty()
        {
            //Arrange
            var model = "Toyota Corolla";
            LicensePlate licensePlate = new LicensePlate("ABC1D23");
            DailyRate dailyRate = null;
            //Act
            Action act = () => new Car(model, licensePlate, dailyRate);
            //Assert
            act.Should().Throw<Exception>().WithMessage("Daily rate cannot be null.");
        }

        [Fact]
        public void MarkAsRented_ShouldSetIsAvailableToFalse()
        {
            //Arrange
            var model = "Toyota Corolla";
            LicensePlate licensePlate = new LicensePlate("ABC1D23");
            DailyRate dailyRate = new DailyRate(50.0m);
            var car = new Car(model, licensePlate, dailyRate);
            //Act
            car.MarkAsRented();
            //Assert
            car.IsAvailable.Should().BeFalse();
        }

        [Fact]
        public void MarkAsRented_ShouldThrowDomainException_WhenCarIsAlreadyRented()
        {
            //Arrange
            var model = "Toyota Corolla";
            LicensePlate licensePlate = new LicensePlate("ABC1D23");
            DailyRate dailyRate = new DailyRate(50.0m);
            var car = new Car(model, licensePlate, dailyRate);
            car.MarkAsRented();
            //Act
            Action act = () => car.MarkAsRented();
            //Assert
            act.Should().Throw<Exception>().WithMessage("Car is already rented.");
        }

        [Fact]
        public void ChangeDailyRate_ShouldUpdateDailyRate()
        {
            //Arrange
            var model = "Toyota Corolla";
            LicensePlate licensePlate = new LicensePlate("ABC1D23");
            DailyRate dailyRate = new DailyRate(50.0m);
            var car = new Car(model, licensePlate, dailyRate);
            DailyRate newDailyRate = new DailyRate(60.0m);
            //Act
            car.ChangeDailyRate(newDailyRate);
            //Assert
            car.DailyRate.Should().Be(newDailyRate);
        }

        [Fact]
        public void ChangeDailyRate_ShouldThrowDomainException_WhenNewDailyRateIsNull()
        {
            //Arrange
            var model = "Toyota Corolla";
            LicensePlate licensePlate = new LicensePlate("ABC1D23");
            DailyRate dailyRate = new DailyRate(50.0m);
            var car = new Car(model, licensePlate, dailyRate);
            DailyRate newDailyRate = null;
            //Act
            Action act = () => car.ChangeDailyRate(newDailyRate);
            //Assert
            act.Should().Throw<Exception>().WithMessage("New daily rate cannot be null.");
        }

    }
}   
