using MediatR;
using OlymppMarketsAPI.Domain.Entities;
using OlymppMarketsAPI.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlymppMarketsAPI.Application.Commands
{
    public class RegisterUserCommand : IRequest<string>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    
}
