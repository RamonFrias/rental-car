using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCar.Application.DTOs.Response
{
    public class RentalResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public Guid CarId { get; set; }
        public string CarModel { get; set; }
        public string CarLicensePlate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
