using RentalCar.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace RentalCar.Domain.ValueObjects
{
    public class ZipCode
    {
        public string Value { get; }

        public ZipCode(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new DomainException("Zip code cannot be null or empty.");
            if (!Regex.IsMatch(value, @"^\d{5}-\d{3}$"))
                throw new DomainException("Zip code must be in the format 12345-678.");
            Value = value;
        }

        public override string ToString() => Value;
    }
}
