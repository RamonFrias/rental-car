using RentalCar.Domain.Exceptions;
using RentalCar.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCar.Domain.Entities
{
    public sealed class Car : Entity
    {
        public string Model { get; private set; }
        public LicensePlate LicensePlate { get; private set; }
        public DailyRate DailyRate { get; private set; }
        public bool IsAvailable { get; private set; }

        private Car() { }

        public Car(string model, LicensePlate licensePlate, DailyRate dailyRate)
        {
            if(string.IsNullOrWhiteSpace(model))
                throw new DomainException("Model cannot be null or empty.");
            if (licensePlate == null)
                throw new DomainException("License plate cannot be null.");
            if (dailyRate == null)
                throw new DomainException("Daily rate cannot be null.");

            Model = model;
            LicensePlate = licensePlate;
            DailyRate = dailyRate;
            IsAvailable = true;
        }

        public void MarkAsRented()
        {
            if (!IsAvailable)
                throw new DomainException("Car is already rented.");
            IsAvailable = false;
        }

        public void MarkAsReturned() => IsAvailable = true;

        public void ChangeDailyRate(DailyRate newDailyRate)
        {
            if (newDailyRate == null)
                throw new DomainException("New daily rate cannot be null.");
            DailyRate = newDailyRate;
        }

    }
}
