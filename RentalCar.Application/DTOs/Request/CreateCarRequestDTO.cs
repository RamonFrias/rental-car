using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RentalCar.Application.DTOs.Request
{
    public class CreateCarRequestDTO
    {
        [Required(ErrorMessage = "Model is required.")]
        [MaxLength(100, ErrorMessage = "Model must not exceed 100 characters.")]
        public string Model { get; set; }
        [Required(ErrorMessage = "License plate is required.")]
        [MaxLength(10, ErrorMessage = "License plate must not exceed 10 characters.")]
        public string LicensePlate { get; set; }
        [Required(ErrorMessage = "Daily rate is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Daily rate must be a positive number.")]
        public decimal DailyRate { get; set; }
    }
}
