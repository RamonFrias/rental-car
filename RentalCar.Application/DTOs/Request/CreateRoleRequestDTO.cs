using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RentalCar.Application.DTOs.Request
{
    public class CreateRoleRequestDTO
    {
        [Required(ErrorMessage = "Role name is required.")]
        public string? RoleName { get; set; }
    }
}
