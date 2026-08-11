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
    public interface IRentalService
    {
        Task<PagedList<RentalResponse>> GetAllRentalsAsync(RentalPaginationParameter parameters, CancellationToken ct = default);
        Task<RentalResponse?> GetRentalByIdAsync(Guid id, CancellationToken ct = default);
        Task<PagedList<RentalResponse>> GetRentalsByUserIdAsync(Guid userId, RentalPaginationParameter parameters, CancellationToken ct = default);
        Task<PagedList<RentalResponse>> GetRentalsByCarIdAsync(Guid carId, RentalPaginationParameter parameters, CancellationToken ct = default);
        Task<PagedList<RentalResponse>> GetRentalsByDateRangeAsync(DateRangeParameter dateRange, RentalPaginationParameter parameters, CancellationToken ct = default);
        Task<RentalResponse> AddRentalAsync(CreateRentalRequestDTO rentaRquestlDTO, CancellationToken ct = default);
        Task UpdateRentalAsync(UpdateRentalRequestDTO updateRentalRequestDTO, CancellationToken ct = default);
        Task DeleteRentalById(Guid id, CancellationToken ct = default);
    }
}
