using System;

namespace BookBook.Dtos.Review
{
    public record ReviewDto
    {
        public Guid MovieID { get; init; }
        public string UserName { get; init; }
        public int Star { get; init; }
        public string Comment { get; init; }
    }
}
