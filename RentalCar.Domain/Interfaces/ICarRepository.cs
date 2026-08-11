using RentalCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCar.Domain.Interfaces
{
    public interface ICarRepository : IRepository<Car>
    {
        Task<Car?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<Car?> GetByLicensePlateAsync(string licensePlate, CancellationToken ct = default);
        Task<(IEnumerable<Car> items, int totalCount)> GetAvailableAsync(int pageNumber, int pageSize, CancellationToken ct = default);
    }
}
