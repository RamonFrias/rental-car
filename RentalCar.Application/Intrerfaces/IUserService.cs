using RentalCar.Application.DTOs.Request;
using RentalCar.Application.DTOs.Response;
using RentalCar.Application.Pagination;
using RentalCar.Application.Pagination.Parameters;
using RentalCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCar.Application.Intrerfaces
{
    public interface IUserService
    {
        Task<PagedList<UserResponseDTO>> GetAllUsersAsync(UserPaginationParameter parameter, CancellationToken ct = default);
        Task<UserResponseDTO?> GetUserByIdAsync(Guid id, CancellationToken ct = default);
        Task<UserResponseDTO> AddUserAsync(CreateUserRequestDTO user, CancellationToken ct = default);
        Task UpdateUserAsync(UpdateUserRequestDTO user, CancellationToken ct = default);
        Task DeleteUserByIdAsync(Guid id, CancellationToken ct = default);
    }
}
