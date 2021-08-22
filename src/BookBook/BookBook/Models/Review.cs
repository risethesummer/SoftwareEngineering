using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookBook.Models
{
    public record Review
    {
        public Guid MovieID { get; init; }
        [ForeignKey("MovieID")]
        public virtual Movie Movie { get; init; }

        public Guid UserID { get; init; }
        [ForeignKey("UserID")]
        public virtual UserAccount User { get; init; }

        [Range(1, 5)]
        public int Star { get; set; }

        [StringLength(1000)]
        public string Comment { get; set; }

    }
}
