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
    public interface ICarService
    {
        Task<PagedList<CarResponseDTO>> GetAllCarsAsync(CarPginationParameter parameters, CancellationToken ct = default);
        Task<CarResponseDTO?> GetCarByIdAsync(Guid id, CancellationToken ct = default);
        Task<CarResponseDTO?> GetCarByLicensePlateAsync(string licensePlate, CancellationToken ct = default);
        Task<PagedList<CarResponseDTO>> GetCarsIsAvailableAsync(CarPginationParameter parameters, CancellationToken ct = default);
        Task<CarResponseDTO> AddCarAsync(CreateCarRequestDTO carDto, CancellationToken ct = default);
        Task UpdateCarAsync(UpdateCarRequestDTO carDto, CancellationToken ct = default);
        Task DeleteCarByIdAsync(Guid id, CancellationToken ct = default);
    }
}
