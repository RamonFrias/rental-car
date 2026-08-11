using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCar.Application.Pagination.Parameters
{
    public class PaginationParameter
    {
        private const int MaxPageSize = 50;
        private int _pageNumber = 1;
        public int Pagenumber
        {
            get => _pageNumber;
            set => _pageNumber = (value < 1) ? 1 : value;
        }
        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}