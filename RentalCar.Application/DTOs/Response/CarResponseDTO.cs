using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCar.Application.DTOs.Response
{
    public class CarResponseDTO
    {
        public Guid Id { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }
        public decimal DailyRate { get; set; }
        public bool Available { get; set; }
    }
}
