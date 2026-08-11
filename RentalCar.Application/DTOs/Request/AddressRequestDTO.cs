using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;
using System.Text;

namespace RentalCar.Application.DTOs.Request
{
    public class AddressRequestDTO
    {
        [Required(ErrorMessage = "Street is required")]
        [MaxLength(200)]
        public string Street { get; set; }
        [Required(ErrorMessage = "Number is required")]
        public int Number { get; set; }
        [Required(ErrorMessage = "City is required")]
        [MaxLength(100)]
        public string City { get; set; }
        [Required(ErrorMessage = "Zip Code is required")]
        [MaxLength(10)]
        public string ZipCode { get; set; }
        [Required(ErrorMessage = "State is required")]
        [MaxLength(2)]
        public string State { get; set; }
    }
}
