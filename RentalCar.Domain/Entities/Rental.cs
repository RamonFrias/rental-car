using RentalCar.Domain.Exceptions;
using RentalCar.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCar.Domain.Entities
{
    public sealed class Rental : Entity
    {
        public Guid UserId { get; private set; }
        public User User { get; private set; }
        public Guid CarId { get; private set; }
        public Car Car { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public decimal Discount { get; private set; }
        public decimal TotalAmount { get; private set; }

        private Rental() { }

        public Rental(User user, Car car, DateTime startDate, DateTime endDate, decimal discount)
        {
            if(user is null)
                throw new DomainException("User cannot be null.");
            if(car is null)
                throw new DomainException("Car cannot be null.");
            if (startDate.Date < DateTime.UtcNow.Date)
                throw new DomainException("Start date cannot be in the past.");
            if (endDate <= startDate)
                throw new DomainException("End date must be after start date.");
            if (discount < 0)
                throw new DomainException("Discount cannot be negative.");

            UserId = user.Id;
            User = user;
            CarId = car.Id;
            Car = car;
            StartDate = startDate;
            EndDate = endDate;
            Discount = discount;
            CalculateTotalAmount(startDate, endDate, car.DailyRate, discount);
        }

        private void CalculateTotalAmount(DateTime startDate, DateTime endDate, DailyRate dailyRate, decimal discount)
        {
            var duration = (endDate.Date - startDate.Date).Days;
            var totalAmount = ((decimal)duration * dailyRate.Value) - discount;
            if(totalAmount < 0)
                throw new DomainException("Total amount cannot be negative.");
            TotalAmount = totalAmount;
        }

        public void Reschedule(DateTime newStartDate, DateTime newEndDate)
        {
            if (newStartDate.Date < DateTime.UtcNow.Date)
                throw new DomainException("Start date cannot be in the past.");
            if (newEndDate <= newStartDate)
                throw new DomainException("End date must be after start date.");

            StartDate = newStartDate;
            EndDate = newEndDate;
            CalculateTotalAmount(newStartDate, newEndDate, Car.DailyRate, Discount);
        }

        public void ChangeCar(Car newCar)
        {
            if (newCar is null)
                throw new DomainException("Car cannot be null.");
            CarId = newCar.Id;
            Car = newCar;
            CalculateTotalAmount(StartDate, EndDate, newCar.DailyRate, Discount);
        }
    }
}
