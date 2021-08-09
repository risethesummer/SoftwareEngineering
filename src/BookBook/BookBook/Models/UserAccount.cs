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
        public byte[] Password { get; init; }

        [Required, MaxLength(100)]
        public string Name { get; init; }

        [EmailAddress, MaxLength(100)]
        public string Email { get; init; }

        public DateTime DayOfBirth { get; init; }

        [MaxLength(100)]
        public string Address { get; init; }
    }
}
