using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RentalCar.Application.DTOs.Request
{
    public class UpdateAddressRequestDTO
    {
        [Required(ErrorMessage = "Street is required")]
        [MaxLength(200)]
        public string Street { get; }
        [Required(ErrorMessage = "Number is required")]
        public int Number { get; }
        [Required(ErrorMessage = "City is required")]
        [MaxLength(100)]
        public string City { get; }
        [Required(ErrorMessage = "Zip Code is required")]
        [MaxLength(10)]
        public string ZipCode { get; }
        [Required(ErrorMessage = "State is required")]
        [MaxLength(2)]
        public string State { get; }
    }
}
