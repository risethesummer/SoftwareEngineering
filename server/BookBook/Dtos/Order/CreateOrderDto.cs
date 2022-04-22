using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBook.Dtos.Order
{
    public record CreateOrderDto
    {
        public Guid SessionID { get; init; }

        public Guid TheaterID { get; init; }

        public Guid ProductID { get; init; }
    }
}
