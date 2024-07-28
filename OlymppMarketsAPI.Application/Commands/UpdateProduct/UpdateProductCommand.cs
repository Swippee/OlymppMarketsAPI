using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OlymppMarketsAPI.DTOs;

namespace OlymppMarketsAPI.Application.Commands
{
    public class UpdateProductCommand : IRequest<Unit>
    {
        public ProductDTO Product { get; set; }

        public UpdateProductCommand(ProductDTO product)
        {
            Product = product;
        }
    }
}
