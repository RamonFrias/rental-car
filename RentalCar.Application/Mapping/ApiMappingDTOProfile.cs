using AutoMapper;
using RentalCar.Application.DTOs.Request;
using RentalCar.Application.DTOs.Response;
using RentalCar.Domain.Entities;
using RentalCar.Domain.ValueObjects;

namespace RentalCar.Application.Mapping
{
    public class ApiMappingDTOProfile : Profile
    {
        public ApiMappingDTOProfile() 
        {
            CreateMap<User, CreateUserRequestDTO>().ReverseMap();
            CreateMap<User, UpdateUserRequestDTO>().ReverseMap();
            CreateMap<User, UserResponseDTO>().ReverseMap();
            CreateMap<Address, AddressResponseDTO>();
            CreateMap<Car, CreateCarRequestDTO>().ReverseMap();
            CreateMap<Car, UpdateCarRequestDTO>().ReverseMap();
            CreateMap<Car, CarResponseDTO>().ReverseMap();
            CreateMap<Rental, CreateRentalRequestDTO>().ReverseMap();
            CreateMap<Rental, UpdateRentalRequestDTO>().ReverseMap();
            CreateMap<Rental, RentalResponse>().ReverseMap();
            CreateMap<Email, string>().ConvertUsing(vo => vo.Value);
            CreateMap<ZipCode, string>().ConvertUsing(vo => vo.Value);
            CreateMap<State, string>().ConvertUsing(vo => vo.Value);
            CreateMap<LicensePlate, string>().ConvertUsing(vo => vo.Value);
            CreateMap<DailyRate, decimal>().ConvertUsing(vo => vo.Value);
        }
    }
}
