using Microsoft.EntityFrameworkCore;
using RentalCar.Domain.Entities;
using RentalCar.Domain.Interfaces;
using RentalCar.Infraestructure.Context;
using System.Linq.Expressions;

namespace RentalCar.Infraestructure.Persistence.Repositories
{
    public class RentalRepository : Repository<Rental>, IRentalRepository
    {
        public RentalRepository(ApplicationDbContext context) : base(context) { }

        // Query-base com os Include — reusada por todos os métodos
        private IQueryable<Rental> QueryWithIncludes()
            => _context.Set<Rental>()
                   .Include(r => r.Car)
                   .Include(r => r.User);

        // Helper de paginação com filtro (aplica o Include, conta e fatia)
        private async Task<(IEnumerable<Rental> rentals, int totalCount)> GetPagedAsync(
            Expression<Func<Rental, bool>> predicate, int pageNumber, int pageSize, CancellationToken ct)
        {
            var query = QueryWithIncludes().Where(predicate);

            var totalCount = await query.CountAsync(ct);
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return (items, totalCount);
        }

        public async Task<(IEnumerable<Rental> rentals, int totalCount)> GetAllRentalsAsync(
            int pageNumber, int pageSize, CancellationToken ct = default)
        {
            var query = QueryWithIncludes();

            var totalCount = await query.CountAsync(ct);
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return (items, totalCount);
        }

        public async Task<Rental?> GetRentalById(Guid id, CancellationToken ct = default)
            => await QueryWithIncludes().FirstOrDefaultAsync(r => r.Id == id, ct);

        public async Task<(IEnumerable<Rental> rentals, int totalCount)> GetRentalsByCarIdAsync(
            Guid carId, int pageNumber, int pageSize, CancellationToken ct = default)
            => await GetPagedAsync(r => r.CarId == carId, pageNumber, pageSize, ct);

        public async Task<(IEnumerable<Rental> rentals, int totalCount)> GetRentalsByDateRangeAsync(
            DateTime startDate, DateTime endDate, int pageNumber, int pageSize, CancellationToken ct = default)
            => await GetPagedAsync(r => r.StartDate >= startDate && r.EndDate <= endDate, pageNumber, pageSize, ct);

        public async Task<(IEnumerable<Rental> rentals, int totalCount)> GetRentalsByUserIdAsync(
            Guid userId, int pageNumber, int pageSize, CancellationToken ct = default)
            => await GetPagedAsync(r => r.UserId == userId, pageNumber, pageSize, ct);
    }
}