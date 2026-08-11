using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCar.Application.Pagination.Parameters
{
    public class RentalPaginationParameter : PaginationParameter
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
