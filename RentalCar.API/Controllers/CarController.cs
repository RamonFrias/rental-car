using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentalCar.Application.DTOs.Request;
using RentalCar.Application.DTOs.Response;
using RentalCar.Application.Intrerfaces;
using RentalCar.Application.Pagination;
using RentalCar.Application.Pagination.Parameters;

namespace RentalCar.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService) => _carService = carService;

        [HttpGet]
        public async Task<ActionResult<PagedList<CarResponseDTO>>> GetAllCars([FromQuery] CarPginationParameter parameters, CancellationToken ct = default)
        {
            var cars = await _carService.GetAllCarsAsync(parameters, ct);
            AddPaginationHeader<CarResponseDTO>(cars);
            return Ok(cars);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<CarResponseDTO>> GetCarById(Guid id, CancellationToken ct = default)
        {
            var car = await _carService.GetCarByIdAsync(id, ct);
            return car is null ? NotFound("Car not found.") : Ok(car);
        }

        [HttpGet("plate/{licensePlate}")]
        public async Task<ActionResult<CarResponseDTO>> GetCarByLicensePlate(string licensePlate, CancellationToken ct = default)
        {
            var car = await _carService.GetCarByLicensePlateAsync(licensePlate, ct);
            return car is null ? NotFound("Car not found.") : Ok(car);
        }

        [HttpGet("available")]
        public async Task<ActionResult<PagedList<CarResponseDTO>>> GetAllAvailablesCars([FromQuery] CarPginationParameter parameters, CancellationToken ct = default)
        {
            var cars = await _carService.GetCarsIsAvailableAsync(parameters, ct);
            AddPaginationHeader<CarResponseDTO>(cars);
            return Ok(cars);
        }

        [HttpPost]
        public async Task<ActionResult<CarResponseDTO>> AddCar(CreateCarRequestDTO carRequestDTO, CancellationToken ct = default)
        {
            var addedCar = await _carService.AddCarAsync(carRequestDTO, ct);
            return Ok(addedCar);
        }

        [HttpPatch("{id:Guid}")]
        public async Task<ActionResult> UpdateCar(Guid id, UpdateCarRequestDTO carRequestDTO, CancellationToken ct = default)
        {
            carRequestDTO.Id = id;
            await _carService.UpdateCarAsync(carRequestDTO, ct);
            return NoContent();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteCarById(Guid id, CancellationToken ct = default)
        {
            await _carService.DeleteCarByIdAsync(id, ct);
            return NoContent();
        }

        private void AddPaginationHeader<T>(PagedList<T> list)
        {
            var metadata = new
            {
                list.PageSize,
                list.CurrentPage,
                list.TotalPage,
                list.HasNext,
                list.HasPrevious,
                list.TotalCount
            };
            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));
        }
    }
}
