using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookBook.Models
{
    public record Ticket
    {
        [Key]
        public Guid ProductID { get; init; }
        [ForeignKey("ProductID")]
        public virtual Product Product { get; init; }

        public Guid MovieID { get; init; }
        [ForeignKey("MovieID")]
        public virtual Movie Movie { get; init; }

        [StringLength(5)]
        public string Seat { get; init; }

        public DateTime ShowTime { get; set; }
    }
}
