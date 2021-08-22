using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookBook.Dtos.Product;

namespace BookBook.Dtos.Order
{
    public record ItemDto
    {
        public Guid ProductID { get; init; }
        public int Amount { get; init; }
    }
}
