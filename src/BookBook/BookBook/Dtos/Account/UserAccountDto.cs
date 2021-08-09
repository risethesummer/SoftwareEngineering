using System;

namespace BookBook.Dtos
{
    namespace Account
    {
        public record UserAccountDto
        {
            public Guid ID { get; init; }
            public string Account { get; init; }
            public string Name { get; init; }
            public string Email { get; init; }
            public DateTime DayOfBirth { get; init; }
            public string Address { get; init; }
        }
    }

}