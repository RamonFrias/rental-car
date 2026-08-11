using AutoMapper;
using RentalCar.Application.DTOs.Request;
using RentalCar.Application.DTOs.Response;
using RentalCar.Application.Intrerfaces;
using RentalCar.Application.Pagination;
using RentalCar.Application.Pagination.Parameters;
using RentalCar.Domain.Entities;
using RentalCar.Domain.Exceptions;
using RentalCar.Domain.Interfaces;
using RentalCar.Domain.ValueObjects;

namespace RentalCar.Application.Services
{
    public class CarService : ICarService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CarService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedList<CarResponseDTO>> GetAllCarsAsync(CarPginationParameter parameters, CancellationToken ct = default)
        {
            var (cars, totalCount) = await _unitOfWork.CarRepository.GetAllAsync(parameters.Pagenumber, parameters.PageSize, ct);
            var carsDTO = _mapper.Map<IEnumerable<CarResponseDTO>>(cars);
            var carsPaginated = new PagedList<CarResponseDTO>(carsDTO, totalCount, parameters.Pagenumber, parameters.PageSize);
            return carsPaginated;
        }

        public async Task<CarResponseDTO?> GetCarByIdAsync(Guid id, CancellationToken ct = default)
        {
            var car = await _unitOfWork.CarRepository.GetByIdAsync(id, ct);
            return car is null ? null : _mapper.Map<CarResponseDTO?>(car);
        }

        public async Task<CarResponseDTO?> GetCarByLicensePlateAsync(string licensePlate, CancellationToken ct = default)
        {
            var car = await _unitOfWork.CarRepository.GetByLicensePlateAsync(licensePlate, ct);
            var carResponseDTO = _mapper.Map<CarResponseDTO?>(car);
            return carResponseDTO;
        }

        public async Task<PagedList<CarResponseDTO>> GetCarsIsAvailableAsync(CarPginationParameter parameters, CancellationToken ct = default)
        {
            var (cars, totalCount) = await _unitOfWork.CarRepository.GetAvailableAsync(parameters.Pagenumber, parameters.PageSize, ct);
            var carsDTO = _mapper.Map<IEnumerable<CarResponseDTO>>(cars);
            var carsPaginated = new PagedList<CarResponseDTO>(carsDTO, totalCount, parameters.Pagenumber, parameters.PageSize);
            return carsPaginated;
        }

        public async Task<CarResponseDTO> AddCarAsync(CreateCarRequestDTO carDto, CancellationToken ct = default)
        {
            var licensePlate = new LicensePlate(carDto.LicensePlate);
            var existsCar = await _unitOfWork.CarRepository.GetByFilterAsync(c => c.LicensePlate == licensePlate, ct);
            if (existsCar is not null)
                throw new DomainException("LicensePlate aleady exists.");
            var car = new Car(carDto.Model, new LicensePlate(carDto.LicensePlate), new DailyRate(carDto.DailyRate));
            await _unitOfWork.CarRepository.AddAsync(car, ct);
            await _unitOfWork.CommitAsync(ct);
            var carResponseDto = _mapper.Map<CarResponseDTO>(car);
            return carResponseDto;
        }

        public async Task UpdateCarAsync(UpdateCarRequestDTO car, CancellationToken ct = default)
        {
            var carExists = await _unitOfWork.CarRepository.GetByIdAsync(car.Id, ct) 
                ?? throw new DomainException("Car not found.");
            carExists.ChangeDailyRate(new DailyRate(car.DailyRate));
            _unitOfWork.CarRepository.Update(carExists);
            await _unitOfWork.CommitAsync(ct);
        }

        public async Task DeleteCarByIdAsync(Guid id, CancellationToken ct = default)
        {
            var car = await _unitOfWork.CarRepository.GetByIdAsync(id, ct) 
                        ?? throw new DomainException("Car not found");
            _unitOfWork.CarRepository.Delete(car);
            await _unitOfWork.CommitAsync(ct);
        }
    }
}
