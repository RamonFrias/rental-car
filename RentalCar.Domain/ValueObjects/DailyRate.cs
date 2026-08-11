using RentalCar.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCar.Domain.ValueObjects
{
    public sealed record DailyRate
    {
        public decimal Value { get; }

        public DailyRate(decimal value)
        {
            if (value < 0)
                throw new DomainException("Daily rate cannot be negative.");
            Value = value;
        }
    }
}
