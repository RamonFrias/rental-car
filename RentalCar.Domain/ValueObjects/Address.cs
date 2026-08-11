using RentalCar.Domain.Exceptions;
using RentalCar.Domain.ValueObjects;

namespace RentalCar.Domain.ValueObjects
{
    public sealed record Address
    {
        public string Street { get; }
        public int Number { get; }
        public string City { get; }
        public ZipCode ZipCode { get; }
        public State State { get; }

        private Address() { }
        public Address(string street, int number, string city, ZipCode zipCode, State state)
        {
            if(zipCode is null)
                throw new DomainException("Zip code cannot be null.");
            if(state is null)
                throw new DomainException("State cannot be null."); 
            if (string.IsNullOrWhiteSpace(street))
                throw new DomainException("Street cannot be null or empty.");
            if (number <= 0)
                throw new DomainException("Number must be greater than zero.");
            if (string.IsNullOrWhiteSpace(city))
                throw new DomainException("City cannot be null or empty.");
            ZipCode = zipCode ?? throw new DomainException(nameof(zipCode));
            State = state ?? throw new DomainException(nameof(state));
            Street = street;
            Number = number;
            City = city;
        }
    }
}
