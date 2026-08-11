using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RentalCar.Application.DTOs.Request
{
    public class CreateUserRequestDTO
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Birth Date is required")]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public AddressRequestDTO Address { get; set; }
    }
}
