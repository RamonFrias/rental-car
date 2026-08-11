using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RentalCar.Application.DTOs.Request
{
    public class CreateRentalRequestDTO
    {
        [Required(ErrorMessage = "User ID is required")]
        public Guid UserId { get; set; }
        [Required(ErrorMessage = "Car ID is required")]
        public Guid CarId { get; set; }
        [Required(ErrorMessage = "Start Date is required")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "End Date is required")]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Discount is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Discount must be between 0 and 100")]
        public decimal Discount { get; set; }
    }
}
