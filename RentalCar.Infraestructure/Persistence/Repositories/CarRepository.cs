using RentalCar.Domain.Entities;
using RentalCar.Domain.Interfaces;
using RentalCar.Domain.ValueObjects;
using RentalCar.Infraestructure.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCar.Infraestructure.Persistence.Repositories
{
    public class CarRepository : Repository<Car>, ICarRepository
    {
        public CarRepository(ApplicationDbContext context) : base(context) { }

        public async Task<(IEnumerable<Car> items, int totalCount)> GetAvailableAsync(int pageNumber, int pageSize, CancellationToken ct = default) 
                => await GetAllByFilterAsync(pageNumber, pageSize, c => c.IsAvailable, ct);

        public async Task<Car?> GetByIdAsync(Guid id, CancellationToken ct = default) 
                => await GetByFilterAsync(c => c.Id == id, ct);

        public async Task<Car?> GetByLicensePlateAsync(string licensePlate, CancellationToken ct = default)
        {
            var normalizedLicensePlate = new LicensePlate(licensePlate).Value;
            return await GetByFilterAsync(c => c.LicensePlate.Value == normalizedLicensePlate, ct);
        }
    }
}
