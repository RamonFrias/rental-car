using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCar.Application.DTOs.Response
{
    public class TokenResponseDTO
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
