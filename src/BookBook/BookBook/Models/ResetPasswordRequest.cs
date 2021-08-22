using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookBook.Models
{
    public record ResetPasswordRequest
    {
        [Key]
        public Guid UserId { get; init; }

        [ForeignKey("UserId")]
        public virtual UserAccount User { get; init; }

        public int Code { get; init; }

        public DateTime Time { get; init; }
    }
}
