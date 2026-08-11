using RentalCar.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace RentalCar.Domain.ValueObjects
{
    public sealed record LicensePlate
    {
        public string Value { get; }

        public LicensePlate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("License plate cannot be null or whitespace.");
            var normalize = value.Trim().ToUpper().Replace("-", "");
            if (!Regex.IsMatch(normalize, @"^[A-Z]{3}[0-9][A-Z][0-9]{2}$"))
                throw new DomainException("License plate must be in the format 'ABC1234' or 'ABC1D23'.");
            Value = normalize;
        }

        public override string ToString() => Value;
    }
}
