using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentalCar.Application.DTOs.Request;
using RentalCar.Application.DTOs.Response;
using RentalCar.Application.Intrerfaces;
using RentalCar.Infraestructure.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RentalCar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthController(ITokenService tokenService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDTO.Username!);
            if(user is not null && await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password!))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName!),
                    new Claim(ClaimTypes.Email, user.Email!),
                    new Claim("id", user.UserName!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                var token = _tokenService.GenerateAccessToken(authClaims, _configuration);
                var refreshToken = _tokenService.GenerateRefreshToken();
                _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInMinutes"], out int refreshTokenValidityInMinutes);
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(refreshTokenValidityInMinutes);
                await _userManager.UpdateAsync(user);
                return Ok(new
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    RefreshToken = refreshToken,
                    Expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register([FromBody] RegisterRequestDTO registerDTO)
        {
            var existsUser = await _userManager.FindByEmailAsync(registerDTO.Email!);
            if(existsUser != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO { Status = "Error", Message = "User already exists." });
            ApplicationUser user = new()
            {
                Email = registerDTO.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerDTO.UserName
            };
            var result = await _userManager.CreateAsync(user, registerDTO.Password!);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO { Status = "Error", Message = "User creation failed." });
            return Ok(new ResponseDTO { Status = "Success", Message = "User created successfully." });
        }

        
        [HttpPost]
        [Route("refresh-token")]
        public async Task<ActionResult> RefreshToken(TokenResponseDTO tokenDTO)
        {
            if (tokenDTO is null)
                return BadRequest("Invalid client request.");
            string? accessToken = tokenDTO.AccessToken ?? throw new ArgumentNullException(nameof(tokenDTO));
            string? refreshToken = tokenDTO.RefreshToken ?? throw new ArgumentNullException(nameof(tokenDTO));
            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken, _configuration);
            if (principal == null)
                return BadRequest("Invalid access token/refresh token.");
            string username = principal.Identity.Name;
            var user = await _userManager.FindByNameAsync(username!);
            var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims.ToList(), _configuration);
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;
            return new ObjectResult(new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                refreshToken = newRefreshToken
            });
        }

        [Authorize]
        [HttpPost]
        [Route("revoke/{userName}")]
        public async Task<ActionResult> Revoke(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return BadRequest("Invalid user name");
            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);
            return NoContent();
        }

        [Authorize(Policy = "ExclusiveAccess")]
        [HttpPost]
        [Route("create-role")]
        public async Task<ActionResult> CreateRole([FromBody] CreateRoleRequestDTO roleNameDTO)
        {
            var roleExists = await _roleManager.RoleExistsAsync(roleNameDTO.RoleName!);
            if (!roleExists)
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleNameDTO.RoleName!));
                if (roleResult.Succeeded)
                    return StatusCode(StatusCodes.Status200OK, new ResponseDTO { Status = "Success", Message = "Role added successfully" });
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDTO { Status = "Error", Message = "Issue adding the new role" });
            }
            return StatusCode(StatusCodes.Status400BadRequest, new ResponseDTO { Status = "Error", Message = "Role already exists" });
        }


        [Authorize(Policy = "ExclusiveAccess")]
        [HttpPost]
        [Route("user-to-role")]
        public async Task<ActionResult> AddUserToRole([FromBody] UserToRoleRequestDTO requestDTO)
        {
            var user = await _userManager.FindByEmailAsync(requestDTO.Email!);
            if (user != null)
            {
                var roleExists = await _roleManager.RoleExistsAsync(requestDTO.Role!);
                if (!roleExists)
                    return StatusCode(StatusCodes.Status404NotFound, new ResponseDTO { Status = "Error", Message = "Role not found." });
                var result = await _userManager.AddToRoleAsync(user, requestDTO.Role!);
                if(result.Succeeded)
                    return StatusCode(StatusCodes.Status200OK, new ResponseDTO { Status = "Success", Message = "User added to role." });
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseDTO { Status = "Error", Message = "Unable to add user to role." });
            }
            return StatusCode(StatusCodes.Status404NotFound, new ResponseDTO { Status = "Error", Message = "User not found." });
        }
    }
}
