using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RentalCar.Application.DTOs.Request
{
    public class UpdateCarRequestDTO
    {
        [Required(ErrorMessage = "Car id is required.")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Daily rate is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Daily rate cannot be negative.")]
        public decimal DailyRate { get; set; }
    }
}
