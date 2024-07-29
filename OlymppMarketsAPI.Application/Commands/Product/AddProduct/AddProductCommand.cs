using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace OlymppMarketsAPI.Application.Commands
{
    public class AddProductCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
