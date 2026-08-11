using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RentalCar.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<(IEnumerable<T> items, int totalCount)> GetAllAsync(int pageNumber, int pageSize, CancellationToken ct = default);
        Task<(IEnumerable<T> items, int totalCount)> GetAllByFilterAsync(int pageNumber, int pageSize, Expression<Func<T, bool>> predicate, CancellationToken ct = default);
        Task<T?> GetByFilterAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default);
        Task<T> AddAsync(T entity, CancellationToken ct = default);
        void Update(T entity);
        void Delete(T entity);
    }
}
