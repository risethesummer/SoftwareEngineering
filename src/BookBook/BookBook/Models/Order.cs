using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookBook.Models
{
    public record Order
    {
        [Key]
        public Guid ID { get; init; }

        public Guid UserID { get; init; }
        [ForeignKey("UserID")]
        public virtual UserAccount User { get; init; }


        public DateTime PurchasedTime { get; set; } = DateTime.Now;

        public bool IsPurchased { get; set; } = false;

        [Range(0, double.MaxValue)]
        public long TotalPrice { get; set; }
    }
}
