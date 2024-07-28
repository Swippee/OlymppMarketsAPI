using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using OlymppMarketsAPI.DTOs;
using OlymppMarketsAPI.Infrastructure.Repositories;
using OlymppMarketsAPI.Domain.Entities;
using OlymppMarketsAPI.Domain.Interfaces;

namespace OlymppMarketsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        public AuthorizationController(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestDTO request)
        {
            var user = await _userRepository.GetByUsernameAsync(request.Username);
            if (user == null || user.Password != request.Password) // Hash and verify password in a real application
            {
                return Unauthorized();
            }

            var token = GenerateJwtToken(user.Username);
            return Ok(new UserLoginResponseDTO { Token = token });

        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] UserRegisterRequestDTO request)
        {
            // Check if the user already exists
            var existingUser = await _userRepository.GetByUsernameAsync(request.Username);
            if (existingUser != null)
            {
                return BadRequest("Username is already taken");
            }

            // Hash the password before saving it (this example uses plain text, you should hash it)
            var user = new User
            {
                Username = request.Username,
                Password = request.Password // You should hash the password
            };

            // Save the user to the database
            await _userRepository.AddAsync(user);

            // Generate JWT token
            var token = GenerateJwtToken(user.Username);

            return Ok(new UserLoginResponseDTO { Token = token });
        }

        private string GenerateJwtToken(string username)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["DurationInMinutes"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
