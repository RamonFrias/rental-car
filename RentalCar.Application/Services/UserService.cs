using AutoMapper;
using RentalCar.Application.DTOs.Request;
using RentalCar.Application.DTOs.Response;
using RentalCar.Application.Intrerfaces;
using RentalCar.Application.Pagination;
using RentalCar.Application.Pagination.Parameters;
using RentalCar.Domain.Entities;
using RentalCar.Domain.Interfaces;
using RentalCar.Domain.ValueObjects;
using RentalCar.Domain.Exceptions;

namespace RentalCar.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedList<UserResponseDTO>> GetAllUsersAsync(UserPaginationParameter parameter, CancellationToken ct = default)
        {
            var (users, totalCount) = await _unitOfWork.UserRepository.GetAllAsync(parameter.Pagenumber, parameter.PageSize, ct);
            var userDtos = _mapper.Map<IEnumerable<UserResponseDTO>>(users);
            return new PagedList<UserResponseDTO>(userDtos, totalCount, parameter.Pagenumber, parameter.PageSize);
        }

        public async Task<UserResponseDTO?> GetUserByIdAsync(Guid id, CancellationToken ct = default)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id, ct);
            var userDto = user is null ? null : _mapper.Map<UserResponseDTO>(user);
            return userDto;
        }

        public async Task<UserResponseDTO> AddUserAsync(CreateUserRequestDTO dto, CancellationToken ct = default)
        {
            var email = new Email(dto.Email);
            var existingUser = await _unitOfWork.UserRepository.GetByFilterAsync(u => u.Email == email, ct);
            if (existingUser is not null)
                throw new DomainException("A user with this email already exists.");
            var address = new Address(
                 dto.Address.Street, dto.Address.Number, dto.Address.City, 
                 new ZipCode(dto.Address.ZipCode), new State(dto.Address.State));
            var user = new User(dto.Name, new Email(dto.Email), dto.BirthDate, address);
            await _unitOfWork.UserRepository.AddAsync(user, ct);
            await _unitOfWork.CommitAsync(ct);
            return _mapper.Map<UserResponseDTO>(user);
        }

        public async Task UpdateUserAsync(UpdateUserRequestDTO dto, CancellationToken ct = default)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(dto.Id, ct)
                ?? throw new DomainException("User not found.");

            if (dto.Name is not null)
                user.ChangeName(dto.Name);

            if (dto.Email is not null)
                user.ChangeEmail(new Email(dto.Email));            

            if (dto.Address is not null)
                user.ChangeAddress(new Address(
                    dto.Address.Street, dto.Address.Number,
                    dto.Address.City, new ZipCode(dto.Address.ZipCode),
                    new State(dto.Address.State)));

            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.CommitAsync(ct);
        }

        public async Task DeleteUserByIdAsync(Guid id, CancellationToken ct = default)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id, ct)
                        ?? throw new DomainException("User not found.");
            _unitOfWork.UserRepository.Delete(user);
            await _unitOfWork.CommitAsync(ct);
        }
    }
}
