using System;

namespace BookBook.Dtos
{
    namespace Account
    {
        public record CreateAccountDto
        {
            public string Account { get; init; }
            public string Password { get; init; }
            public string Name { get; init; }
            public string Email { get; init; }
            public DateTime DayOfBirth { get; init; }
            public string Address { get; init; }
        }
    }

}