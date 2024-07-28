using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlymppMarketsAPI.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Size { get; set; }
        public Price Price { get; set; }
        public Stock Stock { get; set; }
    }
}
