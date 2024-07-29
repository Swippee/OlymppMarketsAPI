using MediatR;
using OlymppMarketsAPI.Application.Commands;
using OlymppMarketsAPI.Application.Services;
using OlymppMarketsAPI.Domain.Entities;
using OlymppMarketsAPI.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace OlymppMarketsAPI.Application.Handlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public RegisterUserCommandHandler(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetByUsernameAsync(request.Username);
            if (existingUser != null)
            {
                throw new System.Exception("Username is already taken");
            }

            var user = new User
            {
                Username = request.Username,
                Password = request.Password // You should hash the password
            };

            await _userRepository.AddAsync(user);
            var token = _tokenService.GenerateJwtToken(user.Username);
            return token;
        }
    }
}

