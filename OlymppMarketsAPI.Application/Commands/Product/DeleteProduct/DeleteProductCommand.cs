using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace OlymppMarketsAPI.Application.Commands
{
    public record DeleteProductCommand(int Id) : IRequest<Unit> { }
}
