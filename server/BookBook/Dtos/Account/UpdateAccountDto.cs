using System;

namespace BookBook.Dtos
{
    namespace Account
    {
        public record UpdateAccountDto
        {
            public string Name { get; init; }
            public string Email { get; init; }
            public string DayOfBirth { get; init; }
            public string Address { get; init; }
        }
    }
}