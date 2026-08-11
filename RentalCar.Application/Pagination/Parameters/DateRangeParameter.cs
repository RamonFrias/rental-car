using RentalCar.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCar.Application.Pagination.Parameters
{
    public sealed record DateRangeParameter
    {
        public DateTime Start { get; }
        public DateTime End { get; }

        public DateRangeParameter(DateTime start, DateTime end)
        {
            if (end < start)
                throw new DomainException("End date must be after or equal to start date.");
            Start = start;
            End = end;
        }
    }
}
