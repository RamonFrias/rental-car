using AutoMapper;
using RentalCar.Application.DTOs.Request;
using RentalCar.Application.DTOs.Response;
using RentalCar.Application.Intrerfaces;
using RentalCar.Application.Pagination;
using RentalCar.Application.Pagination.Parameters;
using RentalCar.Domain.Entities;
using RentalCar.Domain.Exceptions;
using RentalCar.Domain.Interfaces;

namespace RentalCar.Application.Services
{
    public class RentalService : IRentalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RentalService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedList<RentalResponse>> GetAllRentalsAsync(RentalPaginationParameter parameters, CancellationToken ct = default)
        {
            var (rentals, totalCount) = await _unitOfWork.RentalRepository.GetAllRentalsAsync(parameters.Pagenumber, parameters.PageSize, ct);
            var rentalsResponseDTO = _mapper.Map<IEnumerable<RentalResponse>>(rentals);
            var rentalPaginated = new PagedList<RentalResponse>(rentalsResponseDTO, totalCount, parameters.Pagenumber, parameters.PageSize);
            return rentalPaginated;
        }

        public async Task<RentalResponse?> GetRentalByIdAsync(Guid id, CancellationToken ct = default)
        {
            var rental = await _unitOfWork.RentalRepository.GetRentalById(id, ct);
            var rentalResponseDTO = rental is null ? null : _mapper.Map<RentalResponse>(rental);
            return rentalResponseDTO;
        }

        public async Task<PagedList<RentalResponse>> GetRentalsByCarIdAsync(Guid carId, RentalPaginationParameter parameters, CancellationToken ct = default)
        {
            var (rentals, totalCount) = await _unitOfWork.RentalRepository.GetRentalsByCarIdAsync(carId, parameters.Pagenumber, parameters.PageSize, ct);
            var rentalsResponseDTO = _mapper.Map<IEnumerable<RentalResponse>>(rentals);
            var rentalsPaginated = new PagedList<RentalResponse>(rentalsResponseDTO, totalCount, parameters.Pagenumber, parameters.PageSize);
            return rentalsPaginated;
        }

        public async Task<PagedList<RentalResponse>> GetRentalsByDateRangeAsync(DateRangeParameter dateRange, RentalPaginationParameter parameters, CancellationToken ct = default)
        {
            var (rentals, totalCout) = await _unitOfWork.RentalRepository.GetRentalsByDateRangeAsync(dateRange.Start, dateRange.End, parameters.Pagenumber, parameters.PageSize, ct);
            var rentalsResponseDTO = _mapper.Map<IEnumerable<RentalResponse>>(rentals);
            var rentalsPaginated = new PagedList<RentalResponse>(rentalsResponseDTO, totalCout, parameters.Pagenumber, parameters.PageSize);
            return rentalsPaginated;
        }

        public async Task<PagedList<RentalResponse>> GetRentalsByUserIdAsync(Guid userId, RentalPaginationParameter parameters, CancellationToken ct = default)
        {
            var (rentals, totalCount) = await _unitOfWork.RentalRepository.GetRentalsByUserIdAsync(userId, parameters.Pagenumber, parameters.PageSize, ct);
            var rentalsResponseDTO = _mapper.Map<IEnumerable<RentalResponse>>(rentals);
            var rentalsPaginated = new PagedList<RentalResponse>(rentalsResponseDTO, totalCount, parameters.Pagenumber, parameters.PageSize);
            return rentalsPaginated;
        }

        public async Task<RentalResponse> AddRentalAsync(CreateRentalRequestDTO dto, CancellationToken ct = default)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(dto.UserId, ct)
                ?? throw new DomainException("User not found.");

            var car = await _unitOfWork.CarRepository.GetByIdAsync(dto.CarId, ct)
                ?? throw new DomainException("Car not found.");

            if (!car.IsAvailable)
                throw new DomainException("Car is not available.");

            var rental = new Rental(user, car, dto.StartDate, dto.EndDate, dto.Discount);

            car.MarkAsRented();

            await _unitOfWork.RentalRepository.AddAsync(rental, ct);   
            _unitOfWork.CarRepository.Update(car);                      
            await _unitOfWork.CommitAsync(ct);                          

            return _mapper.Map<RentalResponse>(rental);
        }

        public async Task UpdateRentalAsync(UpdateRentalRequestDTO updateRentalRequestDTO, CancellationToken ct = default)
        {
            var rental = await _unitOfWork.RentalRepository.GetRentalById(updateRentalRequestDTO.Id, ct) 
                            ?? throw new DomainException("Rental not found");
            if(updateRentalRequestDTO.StartDate is not null && updateRentalRequestDTO.EndDate is not null)
            {
                var dateRange = new DateRangeParameter(updateRentalRequestDTO.StartDate.Value, updateRentalRequestDTO.EndDate.Value);
                rental.Reschedule(dateRange.Start, dateRange.End);
            }

            if (updateRentalRequestDTO.CarId is not null)
            {
                var newCar = await _unitOfWork.CarRepository.GetByIdAsync(updateRentalRequestDTO.CarId.Value, ct)
                    ?? throw new DomainException("Car not found.");

                if (!newCar.IsAvailable)                       
                    throw new DomainException("Car is not available.");

                var oldCar = rental.Car;                        

                oldCar.MarkAsReturned();                        
                newCar.MarkAsRented();                          
                rental.ChangeCar(newCar);                       

                _unitOfWork.CarRepository.Update(oldCar);       
                _unitOfWork.CarRepository.Update(newCar);
            }

            _unitOfWork.RentalRepository.Update(rental);
            await _unitOfWork.CommitAsync(ct);
        }

        public async Task DeleteRentalById(Guid id, CancellationToken ct = default)
        {
            var rental = await _unitOfWork.RentalRepository.GetRentalById(id)
                            ?? throw new DomainException("Rental not found.");
            _unitOfWork.RentalRepository.Delete(rental);
            await _unitOfWork.CommitAsync(ct);
        }
    }
}
