using RentalCar.Domain.Exceptions;
using RentalCar.Domain.ValueObjects;

namespace RentalCar.Domain.Entities
{
    public sealed class User : Entity
    {
        public string Name { get; private set; }
        public Email Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public Address Address { get; private set; }

        private User() { }

        public User(string name, Email email, DateTime birthDate, Address address)
        {
            if(string.IsNullOrWhiteSpace(name))
                throw new DomainException("Name cannot be null or empty.");
            if(email is null)
                throw new DomainException("Email cannot be null or empty.");
            if(birthDate.Date > DateTime.UtcNow.Date.AddYears(-18))
                throw new DomainException("User must be at least 18 years old.");
            if(address is null)
                throw new DomainException("Address cannot be null.");

            Name = name;
            Email = email;
            BirthDate = birthDate;
            Address = address;
        }

        public void ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Name cannot be null or empty.");
            Name = name;
        }

        public void ChangeEmail(Email email)
        {
            if (email is null)
                throw new DomainException("Email cannot be null or empty.");
            Email = email;
        }

        public void CorrectBirthDate(DateTime birthDate)
        {
            if (birthDate.Date > DateTime.UtcNow.Date.AddYears(-18))
                throw new DomainException("User must be at least 18 years old.");
            BirthDate = birthDate;
        }

        public void ChangeAddress(Address address)
        {
            if (address is null)
                throw new DomainException("Address cannot be null.");
            Address = address;
        }
    }
}
