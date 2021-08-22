using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookBook.Models
{
    public record Person
    {
        [Key]
        public Guid ID { get; init; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required]
        public DateTime DayOfBirth { get; set; }

        [Required, StringLength(20)]
        public string Nation { get; set; }

        [Required, StringLength(1000)]
        public string Description { get; set; }

        public Guid ImageID { get; set; }
    }
}
