using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookBook.Models
{
    public record MovieStaff
    {
        public Guid MovieID { get; init; }
        [ForeignKey("MovieID")]
        public virtual Movie Movie { get; init; }

        public Guid PersonID { get; init; }
        [ForeignKey("PersonID")]
        public virtual Person Person { get; init; }

        public string Role { get; init; }
    }
}
