using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCar.Application.DTOs.Request
{
    public class UpdateRentalRequestDTO
    {
        public Guid Id { get; set; }
        public DateTime? StartDate { get; set; } 
        public DateTime? EndDate { get; set; }
        public Guid? CarId { get; set; }
    }
}
