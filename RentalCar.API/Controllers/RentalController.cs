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
    public class RentalController : ControllerBase
    {
        private readonly IRentalService _rentalService;
        public RentalController(IRentalService rentalService) => _rentalService = rentalService;

        [HttpGet]
        public async Task<ActionResult<PagedList<RentalResponse>>> GetAllRentalsASync([FromQuery] RentalPaginationParameter parameters, CancellationToken ct = default)
        {
            var rentals = await _rentalService.GetAllRentalsAsync(parameters, ct);
            PaginationHeader<RentalResponse>(rentals);
            return Ok(rentals);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<RentalResponse>> GetRentalByIDASync(Guid id, CancellationToken ct = default)
        {
            var rental = await _rentalService.GetRentalByIdAsync(id);
            return rental is null ? NotFound("Rental not found.") : Ok(rental);
        }

        [HttpGet("rentals-by-car-id/{carId:Guid}")]
        public async Task<ActionResult<PagedList<RentalResponse>>> GetRentalsByCarIdASync(Guid carId, [FromQuery] RentalPaginationParameter parameters, CancellationToken ct = default)
        {
            var rentals = await _rentalService.GetRentalsByCarIdAsync(carId, parameters, ct);
            PaginationHeader<RentalResponse>(rentals);
            return Ok(rentals);
        }

        [HttpGet("rentals-by-user-id/{userId:Guid}")]
        public async Task<ActionResult<PagedList<RentalResponse>>> GetRentalsByUserIdASync(Guid userId, [FromQuery] RentalPaginationParameter parameters, CancellationToken ct = default)
        {
            var rentals = await _rentalService.GetRentalsByUserIdAsync(userId, parameters, ct);
            PaginationHeader<RentalResponse>(rentals);
            return Ok(rentals);
        }

        [HttpGet("rentals-by-period/{carId:Guid}")]
        public async Task<ActionResult<PagedList<RentalResponse>>> GetRentalsByPeriodASync(DateRangeParameter dateRangeParameter, [FromQuery] RentalPaginationParameter parameters, CancellationToken ct = default)
        {
            var rentals = await _rentalService.GetRentalsByDateRangeAsync(dateRangeParameter, parameters, ct);
            PaginationHeader<RentalResponse>(rentals);
            return Ok(rentals);
        }

        [HttpPost]
        public async Task<ActionResult<RentalResponse>> AddRentalASync(CreateRentalRequestDTO rentalDTO, CancellationToken ct = default)
        {
            var rental = await _rentalService.AddRentalAsync(rentalDTO, ct);
            return Ok(rental);
        }

        [HttpPatch("change-rental")]
        public async Task<ActionResult> UpdateRentalASync(UpdateRentalRequestDTO rentalDTO, CancellationToken ct = default)
        {
            await _rentalService.UpdateRentalAsync(rentalDTO, ct);
            return NoContent();
        }

        [HttpDelete("delete-rental/{id:Guid}")]
        public async Task<ActionResult> DeleteRentalASync(Guid id, CancellationToken ct = default)
        {
            await _rentalService.DeleteRentalById(id, ct);
            return NoContent();
        }

        private void PaginationHeader<T>(PagedList<T> list)
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
