using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBook.Dtos.Product
{
    public record UpdateTheaterProductDto
    {
        public Guid ProductID { get; init; }
        public Guid TheaterID { get; init; }
        public int Amount { get; init; }
    }
}
