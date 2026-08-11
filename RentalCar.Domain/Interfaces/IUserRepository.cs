using RentalCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCar.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default);
    }
}
