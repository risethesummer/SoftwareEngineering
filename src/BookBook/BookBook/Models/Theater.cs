using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookBook.Models
{
    public record Theater
    {
        [Key]
        public Guid ID { get; init; }
        
        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required, StringLength(100)]
        public string Location { get; set; }
    }
}
