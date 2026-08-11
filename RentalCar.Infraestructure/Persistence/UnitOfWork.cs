using RentalCar.Domain.Interfaces;
using RentalCar.Infraestructure.Context;
using RentalCar.Infraestructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace RentalCar.Infraestructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context) => _context = context;

        private IUserRepository _userRepository;
        private ICarRepository _carRepository;
        private IRentalRepository _rentalRepository;

        public IUserRepository UserRepository
        {
            get => _userRepository ??= new UserRepository(_context);
        }

        public ICarRepository CarRepository
        {
            get => _carRepository ??= new CarRepository(_context);
        }

        public IRentalRepository RentalRepository
        {
            get => _rentalRepository ??= new RentalRepository(_context);
        }

        public async Task CommitAsync(CancellationToken ct) => await _context.SaveChangesAsync(ct);

        public async Task DisposeAsync() => await _context.DisposeAsync();
    }
}
