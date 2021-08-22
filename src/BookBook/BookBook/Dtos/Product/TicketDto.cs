using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBook.Dtos.Product
{
    public record TicketDto : ProductDto
    {
        public IEnumerable<Guid> TheaterID { get; init; }
        public Guid MovieID { get; init; }
        public string ShowTime { get; init; }

    }
}
