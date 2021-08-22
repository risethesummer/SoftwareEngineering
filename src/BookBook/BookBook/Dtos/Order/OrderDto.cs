using System;
using System.Collections.Generic;

namespace BookBook.Dtos.Order
{
    public record OrderDto
    {
        public Guid ID { get; init; }
        public IEnumerable<ItemDto> Items { get; init; }
        public bool IsPurchased { get; set; }
        public string PurchasedTime { get; init; }
        public long TotalPrice { get; init; }
    }
}
