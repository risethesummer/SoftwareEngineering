using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookBook.Models
{
    public record Movie
    {
        [Key]
        public Guid ID { get; init; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, Range(1900, double.PositiveInfinity)]
        public int Year { get; set; }

        [Required, StringLength(20)]
        public string Nation { get; set; }

        [Required, StringLength(20)]
        public string Genre { get; set; }

        [Range(0, 18)]
        public int RequiredAge { get; set; }

        [Required, Range(0, 1000)]
        public int Duration { get; set; }

        [Required, StringLength(1000)]
        public string Description { get; set; }

        public Guid ImageID { get; set; }

        [Range(0, 10)]
        public int IMDBStar { get; set; } = 0;

        [StringLength(100)]
        public string YoutubeLink { get; set; }
    }
}
