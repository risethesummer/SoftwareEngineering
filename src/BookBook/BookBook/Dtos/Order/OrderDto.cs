using System;
using System.Collections.Generic;

namespace BookBook.Dtos.Order
{
    public record OrderDto
    {
        public Guid ID { get; init; }
        public Guid ProductID { get; init; }
        public bool IsPurchased { get; set; }
        public string PurchasedTime { get; init; }
    }
}
