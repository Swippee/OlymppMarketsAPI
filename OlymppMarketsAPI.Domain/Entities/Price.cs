using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlymppMarketsAPI.Domain.Entities
{
    public class Price
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal Amount { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
