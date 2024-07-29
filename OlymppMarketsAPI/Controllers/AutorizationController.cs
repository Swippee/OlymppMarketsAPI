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
using MediatR;
using OlymppMarketsAPI.Application.Commands;

namespace OlymppMarketsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;
        public AuthorizationController(IConfiguration configuration, IMediator mediator)
        {
            _configuration = configuration;
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestDTO request)
        {
            var command = new LoginUserCommand
            {
                Username = request.Username,
                Password = request.Password
            };

            var token = await _mediator.Send(command);
            return Ok(new UserLoginResponseDTO { Token = token });

        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] UserRegisterRequestDTO request)
        {
            var command = new RegisterUserCommand
            {
                Username = request.Username,
                Password = request.Password
            };

            var token = await _mediator.Send(command);
            return Ok(new UserLoginResponseDTO { Token = token });
        }   
    }
}
