using System;

namespace BookBook.Dtos.Product
{
    public record ProductDto
    {
        public Guid ID { get; init; }
        public string Name { get; init; }
        public string Type { get; init; }
        public int Price { get; init; }
    }
}
