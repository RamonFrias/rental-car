using RentalCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCar.Domain.Interfaces
{
    public interface IRentalRepository : IRepository<Rental>
    {
        Task<Rental?> GetRentalById(Guid id, CancellationToken ct = default);
        Task<(IEnumerable<Rental> rentals, int totalCount)> GetAllRentalsAsync(int pageNumber, int pageSize, CancellationToken ct = default);
        Task<(IEnumerable<Rental> rentals, int totalCount)> GetRentalsByUserIdAsync(Guid userId, int pageNumber, int pageSize, CancellationToken ct = default);
        Task<(IEnumerable<Rental> rentals, int totalCount)> GetRentalsByCarIdAsync(Guid carId, int pageNumber, int pageSize, CancellationToken ct = default);
        Task<(IEnumerable<Rental> rentals, int totalCount)> GetRentalsByDateRangeAsync(DateTime startDate, DateTime endDate, int pageNumber, int pageSize, CancellationToken ct = default);
    }
}
