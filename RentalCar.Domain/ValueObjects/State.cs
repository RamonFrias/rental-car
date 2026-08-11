using RentalCar.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCar.Domain.ValueObjects
{
    public sealed record State
    {
        private static readonly HashSet<string> ValidUFs = new()
        {
            "AC","AL","AP","AM","BA","CE","DF","ES","GO","MA","MT","MS","MG",
            "PA","PB","PR","PE","PI","RJ","RN","RS","RO","RR","SC","SP","SE","TO"
        };
        public string Value { get; }

        public State(string value)
        {
            if(string.IsNullOrEmpty(value))
                throw new DomainException("State abbreviation cannot be null or empty.");
            if (!ValidUFs.Contains(value))
                throw new DomainException($"Invalid state abbreviation: {value}");
            Value = value;
        }

        public override string ToString() => Value;
    }
}
