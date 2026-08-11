using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentalCar.Application.DTOs.Request;
using RentalCar.Application.DTOs.Response;
using RentalCar.Application.Intrerfaces;
using RentalCar.Application.Pagination;
using RentalCar.Application.Pagination.Parameters;
using System.Reflection.Metadata.Ecma335;

namespace RentalCar.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        public async Task<ActionResult<PagedList<UserResponseDTO>>> GetAllUsersAsync([FromQuery] UserPaginationParameter parameters, CancellationToken ct = default)
        {
            var users = await _userService.GetAllUsersAsync(parameters, ct);
            var metadata = new
            {
                PageNumber = users.CurrentPage,
                PageSize = users.PageSize,
                TotalPages = users.TotalPage,
                HasNext = users.HasNext,
                HasPreview = users.HasPrevious
            };
            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(users);
        }

        [Authorize(Policy = "AdminOnly")]
        [Authorize(Policy = "UserOnly")]
        [HttpGet("{id:Guid}", Name = "GetUserByIdAsync")]
        public async Task<ActionResult<UserResponseDTO>> GetUserByIdAsync(Guid id, CancellationToken ct = default)
        {
            var user = await _userService.GetUserByIdAsync(id, ct);
            return user is null ? NotFound("User not found") : Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserResponseDTO>> AddUserAsync(CreateUserRequestDTO userRequestDTO, CancellationToken ct = default)
        {
            var addedUser = await _userService.AddUserAsync(userRequestDTO);
            return Ok(addedUser);
        }

        [HttpPatch("{id:Guid}")]

        public async Task<ActionResult> ChangeUserDataById(Guid id, [FromBody] UpdateUserRequestDTO userRequestDTO, CancellationToken ct = default)
        {
            userRequestDTO.Id = id;
            await _userService.UpdateUserAsync(userRequestDTO, ct);
            return NoContent();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteUserByIdAsync(Guid id, CancellationToken ct = default)
        {
            await _userService.DeleteUserByIdAsync(id, ct);
            return NoContent();
        }
    }
}
