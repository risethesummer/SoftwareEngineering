using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookBook.Models
{
    public record TheaterProducts
    {
        public Guid TheaterID { get; init; }
        [ForeignKey("TheaterID")]
        public virtual Theater Theater { get; init; }

        public Guid ProductID { get; init; }
        [ForeignKey("ProductID")]
        public virtual Product Product { get; init; }

        [Range(0, double.MaxValue)]
        public int Remains { get; set; }
    }
}
