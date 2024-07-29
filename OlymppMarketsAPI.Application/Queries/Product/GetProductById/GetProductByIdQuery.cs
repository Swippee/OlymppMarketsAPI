using MediatR;
using OlymppMarketsAPI.Domain.Entities;
using OlymppMarketsAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlymppMarketsAPI.Application.Queries
{
    public record GetProductByIdQuery(int Id) : IRequest<ProductDTO?>
    {
    }
}
