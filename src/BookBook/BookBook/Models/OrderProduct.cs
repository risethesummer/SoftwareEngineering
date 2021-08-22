using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookBook.Models
{
    public record OrderProduct
    {
        public Guid OrderID { get; init; }
        [ForeignKey("OrderID")]
        public virtual Order Order { get; init; }
        
        public Guid ProductID { get; set; }
        [ForeignKey("ProductID")]
        public virtual Product Product {get; set;}

        [Required, Range(0, 100)]
        public int Amount { get; set; }
    }
}
