using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace BookBook.Models
{
    public record UserAccount
    {


        [Key, MaxLength(100)]
        public Guid ID 
        { 
            get; 
            init; 
        }

        [Required, MaxLength(50)]
        public string Account { get; init; }

        [Required, MaxLength(16)]
        public byte[] Password { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [EmailAddress, MaxLength(100)]
        public string Email { get; set; }

        public DateTime DayOfBirth { get; set; }

        [MaxLength(100)]
        public string Address { get; set; }
    }
}
