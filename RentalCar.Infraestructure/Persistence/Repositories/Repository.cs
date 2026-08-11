using Microsoft.EntityFrameworkCore;
using RentalCar.Domain.Interfaces;
using RentalCar.Infraestructure.Context;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RentalCar.Infraestructure.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<T> items, int totalCount)> GetAllByFilterAsync(int pageNumber, int pageSize, Expression<Func<T, bool>> predicate, CancellationToken ct = default)
        {
            var query = _context.Set<T>().Where(predicate);
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(ct);
            var totalCount = await query.CountAsync(ct);
            return (items, totalCount);
        }

        public async Task<(IEnumerable<T> items, int totalCount)> GetAllAsync(int pageNumber, int pageSize, CancellationToken ct = default)
        {
            var query = _context.Set<T>();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(ct);
            var totalCount = await query.CountAsync(ct);
            return (items, totalCount);
        }

        public async Task<T?> GetByFilterAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default)
        {
            var item = await _context.Set<T>().FirstOrDefaultAsync(predicate, ct);
            return item;
        }

        public async Task<T> AddAsync(T entity, CancellationToken ct = default)
        {
            await _context.Set<T>().AddAsync(entity, ct);
            return entity;
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}
