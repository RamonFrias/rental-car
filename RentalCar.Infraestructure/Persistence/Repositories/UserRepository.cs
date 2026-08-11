using RentalCar.Domain.Entities;
using RentalCar.Domain.Interfaces;
using RentalCar.Infraestructure.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCar.Infraestructure.Persistence.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var user = await GetByFilterAsync(u => u.Id == id, ct);
            return user;
        }
    }
}
