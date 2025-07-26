using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SmartFridgeApi.DTOs;
using SmartFridgeApi.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;


namespace SmartFridgeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly string _jwtKey = "super_secret_key_1234567890_very_long_key!";

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest("Email et mot de passe sont obligatoires.");

            var user = _authService.GetUserByEmail(dto.Email);
            if (user != null)
                return BadRequest("User already exists.");
            _authService.Register(dto.Email, dto.Password);
            return Ok("Registration successful.");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            if (!_authService.ValidatePassword(dto.Email, dto.Password))
                return Unauthorized();

            var user = _authService.GetUserByEmail(dto.Email);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(12),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new { token = jwt });
        }
        [HttpGet("users")]
        [Authorize] 
        public IActionResult GetUsers()
        {
            var users = _authService.GetAllUsers();
            return Ok(users);
        }

        [HttpDelete("users/{id}")]
        [Authorize] 
        public IActionResult DeleteUser(int id)
        {
            var ok = _authService.DeleteUser(id);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}
