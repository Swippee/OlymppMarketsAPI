using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlymppMarketsAPI.Domain.Entities
{
    public class Stock
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
