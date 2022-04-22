using System;
using System.ComponentModel.DataAnnotations;

namespace BookBook.Models
{
    public record Product
    {
        [Key]
        public Guid ID { get; init; }
        
        [Required, StringLength(50)]
        public string Name { get; set; }
        

        [Required, StringLength(30)]
        public string Type { get; set; }

        [Required, Range(0, double.MaxValue)]
        public int Price { get; set; }
    }
}
