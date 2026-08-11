using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCar.Domain.Entities
{
    public class Entity
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();
    }
}
