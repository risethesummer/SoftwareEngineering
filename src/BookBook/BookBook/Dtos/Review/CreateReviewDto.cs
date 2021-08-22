using System;

namespace BookBook.Dtos.Review
{
    public record CreateReviewDto
    {
        public Guid SessionID { get; init; }
        public Guid MovieID { get; init; }
        public int Star { get; init; }
        public string Comment { get; init; }
    }
}
