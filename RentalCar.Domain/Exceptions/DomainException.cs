using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCar.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message) { }
    }
}
