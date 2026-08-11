using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCar.Application.DTOs.Response
{
    public class UserResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public AddressResponseDTO Address { get; set; }
    }
}
