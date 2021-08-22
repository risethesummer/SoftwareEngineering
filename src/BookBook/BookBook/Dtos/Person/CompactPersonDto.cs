using System;

namespace BookBook.Dtos.Person
{
    public record CompactPersonDto
    {
        public Guid ID { get; init; } = Guid.Empty;
        public string Name { get; init; } = "";
    }
}
