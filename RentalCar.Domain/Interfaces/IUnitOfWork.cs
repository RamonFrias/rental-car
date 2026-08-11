using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCar.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        ICarRepository CarRepository { get; }
        IRentalRepository RentalRepository { get; }
        Task CommitAsync(CancellationToken ct = default);
        Task DisposeAsync();
    }
}
