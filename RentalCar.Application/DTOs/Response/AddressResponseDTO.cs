using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCar.Application.DTOs.Response
{
    public class AddressResponseDTO
    {
        public string Street { get; set; }
        public int Number { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}
