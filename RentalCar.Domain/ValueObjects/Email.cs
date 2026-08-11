using RentalCar.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace RentalCar.Domain.ValueObjects
{
    public sealed record Email
    {
        public string Value { get; }

        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Email cannot be null or empty.");
            if(!Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new DomainException("Email is not valid.");
            Value = value;
        }

        public override string ToString() => Value;
    }
}
