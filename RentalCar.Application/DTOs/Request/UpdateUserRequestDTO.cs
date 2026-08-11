using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RentalCar.Application.DTOs.Request
{
    public class UpdateUserRequestDTO
    {
        [Required(ErrorMessage = "Id is required")]
        public Guid Id { get; set; }
        [MaxLength(150, ErrorMessage = "Name cannot exceed 150 characters")]
        public string? Name { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public UpdateAddressRequestDTO? Address { get; set; }
    }
}
